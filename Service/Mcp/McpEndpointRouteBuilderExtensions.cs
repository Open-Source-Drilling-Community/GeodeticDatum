using System;
using System.Buffers;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp;

public static class McpEndpointRouteBuilderExtensions
{
    private const string SseTransport = "sse";
    private const string WebSocketTransport = "websocket";

    public static IEndpointRouteBuilder MapMcpEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/mcp");

        group.MapGet(string.Empty, HandleSseAsync)
             .WithName("McpSse")
             .Produces(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status409Conflict)
             .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost(string.Empty, HandleClientPostAsync)
             .WithName("McpClientPost")
             .Accepts<McpClientEnvelope>("application/json")
             .Produces(StatusCodes.Status202Accepted)
             .Produces(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/ws", HandleWebSocketAsync)
             .WithName("McpWebSocket")
             .Produces(StatusCodes.Status101SwitchingProtocols)
             .Produces(StatusCodes.Status409Conflict)
             .Produces(StatusCodes.Status400BadRequest);

        return endpoints;
    }

    private static async Task HandleSseAsync(HttpContext context, McpServer server, ILogger<McpServer> logger)
    {
        McpSession? session = null;
        try
        {
            var handshake = McpHandshakeReader.FromHttpRequest(context.Request);
            session = server.CreateSession(handshake, SseTransport);
        }
        catch (InvalidOperationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsync(ex.Message);
            return;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create MCP SSE session");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Failed to establish MCP session.");
            return;
        }

        context.Response.StatusCode = StatusCodes.Status200OK;
        context.Response.Headers.ContentType = "text/event-stream; charset=utf-8";
        context.Response.Headers.CacheControl = "no-cache";
        context.Response.Headers.Connection = "keep-alive";
        context.Response.Headers["X-Accel-Buffering"] = "no";

        await context.Response.WriteAsync($": session {session.Id} established\n\n");
        await context.Response.Body.FlushAsync(context.RequestAborted);

        try
        {
            await foreach (var message in session.ReadOutboundAsync(context.RequestAborted))
            {
                var json = JsonSerializer.Serialize(message, McpJson.Default);
                await context.Response.WriteAsync("data: ");
                await context.Response.WriteAsync(json);
                await context.Response.WriteAsync("\n\n");
                await context.Response.Body.FlushAsync(context.RequestAborted);
            }
        }
        catch (OperationCanceledException)
        {
            // graceful cancellation when client disconnects
        }
        catch (IOException)
        {
            // client terminated connection
        }
        finally
        {
            server.CompleteSession(session.Id);
        }
    }

    private static async Task<IResult> HandleClientPostAsync(HttpContext context, McpServer server, ILogger<McpServer> logger, CancellationToken cancellationToken)
    {
        McpClientEnvelope? envelope;
        try
        {
            envelope = await JsonSerializer.DeserializeAsync<McpClientEnvelope>(context.Request.Body, McpJson.Default, cancellationToken);
        }
        catch (JsonException ex)
        {
            logger.LogWarning(ex, "Invalid MCP client payload received via POST");
            return Results.BadRequest(new { error = "invalid_payload", message = "Payload could not be parsed." });
        }

        if (envelope is null)
        {
            return Results.BadRequest(new { error = "invalid_payload", message = "Request body is empty." });
        }

        if (string.IsNullOrWhiteSpace(envelope.SessionId))
        {
            return Results.BadRequest(new { error = "missing_session", message = "sessionId is required." });
        }

        try
        {
            await server.HandleClientMessageAsync(envelope, cancellationToken);
            return Results.Accepted();
        }
        catch (McpSessionNotFoundException)
        {
            return Results.NotFound(new { error = "session_not_found", message = "The specified session was not found." });
        }
        catch (McpInvalidMessageException ex)
        {
            return Results.BadRequest(new { error = "invalid_message", message = ex.Message });
        }
    }

    private static async Task HandleWebSocketAsync(HttpContext context, McpServer server, ILogger<McpServer> logger)
    {
        if (!context.WebSockets.IsWebSocketRequest)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Expected a WebSocket request.");
            return;
        }

        McpSession? session = null;
        try
        {
            var handshake = McpHandshakeReader.FromHttpRequest(context.Request);
            session = server.CreateSession(handshake, WebSocketTransport);
        }
        catch (InvalidOperationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsync(ex.Message);
            return;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create MCP WebSocket session");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Failed to establish MCP session.");
            return;
        }

        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(context.RequestAborted);
        using var socket = await context.WebSockets.AcceptWebSocketAsync();

        var sendTask = SendWebSocketMessagesAsync(socket, session, linkedCts.Token);
        var receiveTask = ReceiveWebSocketMessagesAsync(socket, server, session, logger, linkedCts.Token);

        await Task.WhenAny(sendTask, receiveTask);
        linkedCts.Cancel();

        if (socket.State == WebSocketState.Open)
        {
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
        }

        server.CompleteSession(session.Id);

        try
        {
            await Task.WhenAll(sendTask, receiveTask);
        }
        catch (Exception ex)
        {
            logger.LogDebug(ex, "MCP WebSocket shutdown completed with exceptions");
        }
    }

    private static async Task SendWebSocketMessagesAsync(WebSocket socket, McpSession session, CancellationToken cancellationToken)
    {
        await foreach (var message in session.ReadOutboundAsync(cancellationToken))
        {
            var json = JsonSerializer.Serialize(message, McpJson.Default);
            var buffer = Encoding.UTF8.GetBytes(json);
            await socket.SendAsync(buffer, WebSocketMessageType.Text, endOfMessage: true, cancellationToken);
        }
    }

    private static async Task ReceiveWebSocketMessagesAsync(WebSocket socket, McpServer server, McpSession session, ILogger logger, CancellationToken cancellationToken)
    {
        var buffer = ArrayPool<byte>.Shared.Rent(16 * 1024);
        using var messageStream = new MemoryStream();

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = await socket.ReceiveAsync(buffer, cancellationToken);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }

                messageStream.Write(buffer, 0, result.Count);

                if (!result.EndOfMessage)
                {
                    continue;
                }

                var payload = Encoding.UTF8.GetString(messageStream.GetBuffer(), 0, (int)messageStream.Length);
                messageStream.SetLength(0);

                McpClientEnvelope? envelope = null;
                try
                {
                    envelope = JsonSerializer.Deserialize<McpClientEnvelope>(payload, McpJson.Default);
                }
                catch (JsonException ex)
                {
                    logger.LogWarning(ex, "Invalid MCP WebSocket payload received");
                }

                if (envelope is null)
                {
                    await session.SendAsync(McpMessageTypes.Error,
                        new JsonObject
                        {
                            ["code"] = "invalid_payload",
                            ["message"] = "Payload could not be parsed."
                        }, null, cancellationToken);
                    continue;
                }

                if (string.IsNullOrWhiteSpace(envelope.SessionId))
                {
                    envelope = envelope with { SessionId = session.Id };
                }

                try
                {
                    await server.HandleClientMessageAsync(envelope, cancellationToken);
                }
                catch (McpSessionNotFoundException)
                {
                    await session.SendAsync(McpMessageTypes.Error,
                        new JsonObject
                        {
                            ["code"] = "session_not_found",
                            ["message"] = "Session is no longer available."
                        }, envelope.RequestId, cancellationToken);
                }
                catch (McpInvalidMessageException ex)
                {
                    await session.SendAsync(McpMessageTypes.Error,
                        new JsonObject
                        {
                            ["code"] = "invalid_message",
                            ["message"] = ex.Message
                        }, envelope.RequestId, cancellationToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Unexpected error handling MCP WebSocket message");
                    await session.SendAsync(McpMessageTypes.Error,
                        new JsonObject
                        {
                            ["code"] = "internal_error",
                            ["message"] = "An error occurred while handling the message."
                        }, envelope.RequestId, cancellationToken);
                }
            }
        }
        catch (OperationCanceledException)
        {
            // connection was aborted
        }
        catch (IOException)
        {
            // socket closed abruptly
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
            messageStream.Dispose();
        }
    }
}
