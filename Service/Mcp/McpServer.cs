using System;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp;

public class McpServer
{
    private readonly ConcurrentDictionary<string, McpSession> _sessions = new(StringComparer.Ordinal);
    private readonly McpToolRegistry _toolRegistry;
    private readonly ILogger<McpServer> _logger;

    public McpServer(McpToolRegistry toolRegistry, ILogger<McpServer> logger)
    {
        _toolRegistry = toolRegistry;
        _logger = logger;
    }

    public McpSession CreateSession(McpHandshake handshake, string transport)
    {
        var sessionId = string.IsNullOrWhiteSpace(handshake.SessionId)
            ? Guid.NewGuid().ToString("N")
            : handshake.SessionId.Trim();

        if (_sessions.ContainsKey(sessionId))
        {
            throw new InvalidOperationException($"An MCP session with id '{sessionId}' already exists.");
        }

        var session = new McpSession(sessionId, handshake, transport);
        if (!_sessions.TryAdd(session.Id, session))
        {
            throw new InvalidOperationException($"Failed to register MCP session '{session.Id}'.");
        }

        SendSessionCreated(session, transport);
        SendToolList(session);

        _logger.LogInformation("MCP session {SessionId} created using {Transport}", session.Id, transport);

        return session;
    }

    public bool TryGetSession(string sessionId, out McpSession? session)
    {
        return _sessions.TryGetValue(sessionId, out session);
    }

    public void CompleteSession(string sessionId)
    {
        if (_sessions.TryRemove(sessionId, out var session))
        {
            session.Complete();
            _logger.LogInformation("MCP session {SessionId} closed", sessionId);
        }
    }

    public async Task HandleClientMessageAsync(McpClientEnvelope envelope, CancellationToken cancellationToken)
    {
        if (envelope.Type is null)
        {
            throw new McpInvalidMessageException("Client message is missing a 'type' property.");
        }

        if (string.IsNullOrWhiteSpace(envelope.SessionId))
        {
            throw new McpInvalidMessageException("Client message is missing a 'sessionId' property.");
        }

        if (!_sessions.TryGetValue(envelope.SessionId, out var session))
        {
            throw new McpSessionNotFoundException(envelope.SessionId);
        }

        session.Touch();

        switch (envelope.Type)
        {
            case McpClientMessageTypes.Ping:
                await session.SendAsync(McpMessageTypes.Pong,
                    new JsonObject { ["timestamp"] = DateTimeOffset.UtcNow.ToString("O") },
                    envelope.RequestId, cancellationToken);
                break;

            case McpClientMessageTypes.ListTools:
                await SendToolsAsync(session, envelope.RequestId, cancellationToken);
                break;

            case McpClientMessageTypes.InvokeTool:
                await HandleInvokeAsync(session, envelope, cancellationToken);
                break;

            default:
                await SendErrorAsync(session, envelope.RequestId,
                    $"Unknown client message type '{envelope.Type}'.", "unknown_type", cancellationToken);
                break;
        }
    }

    private void SendSessionCreated(McpSession session, string transport)
    {
        var payload = new JsonObject
        {
            ["sessionId"] = session.Id,
            ["transport"] = transport,
            ["protocolVersion"] = session.Handshake.ProtocolVersion,
            ["server"] = new JsonObject
            {
                ["name"] = McpServerConstants.ServerName,
                ["version"] = McpServerConstants.ServerVersion
            }
        };

        if (!string.IsNullOrWhiteSpace(session.Handshake.ClientName) || !string.IsNullOrWhiteSpace(session.Handshake.ClientVersion))
        {
            var client = new JsonObject();
            if (!string.IsNullOrWhiteSpace(session.Handshake.ClientName))
            {
                client["name"] = session.Handshake.ClientName;
            }

            if (!string.IsNullOrWhiteSpace(session.Handshake.ClientVersion))
            {
                client["version"] = session.Handshake.ClientVersion;
            }

            payload["client"] = client;
        }

        if (session.Handshake.Capabilities is { Count: > 0 } capabilities)
        {
            payload["capabilities"] = capabilities.DeepClone();
        }

        session.TrySend(McpMessageTypes.SessionCreated, payload);
    }

    private void SendToolList(McpSession session)
    {
        var tools = _toolRegistry.DescribeTools();
        var toolsNode = JsonSerializer.SerializeToNode(tools, McpJson.Default);
        var payload = new JsonObject
        {
            ["tools"] = toolsNode
        };

        session.TrySend(McpMessageTypes.ToolsList, payload);
    }

    private async Task SendToolsAsync(McpSession session, string? requestId, CancellationToken cancellationToken)
    {
        var tools = _toolRegistry.DescribeTools();
        var toolsNode = JsonSerializer.SerializeToNode(tools, McpJson.Default);
        var payload = new JsonObject
        {
            ["tools"] = toolsNode
        };

        await session.SendAsync(McpMessageTypes.ToolsList, payload, requestId, cancellationToken);
    }

    private async Task HandleInvokeAsync(McpSession session, McpClientEnvelope envelope, CancellationToken cancellationToken)
    {
        try
        {
            var (toolName, arguments) = ParseInvocationPayload(envelope.Payload);
            var result = await _toolRegistry.InvokeAsync(toolName, arguments, cancellationToken);

            var payload = new JsonObject
            {
                ["tool"] = toolName,
                ["result"] = result
            };

            await session.SendAsync(McpMessageTypes.ToolResult, payload, envelope.RequestId, cancellationToken);
        }
        catch (McpToolNotFoundException ex)
        {
            _logger.LogWarning("Client attempted to invoke MCP tool {ToolName} that is not registered", ex.ToolName);
            await SendErrorAsync(session, envelope.RequestId, ex.Message, "tool_not_found", cancellationToken);
        }
        catch (McpInvalidMessageException ex)
        {
            await SendErrorAsync(session, envelope.RequestId, ex.Message, "invalid_request", cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to execute MCP tool for session {SessionId}", session.Id);
            await SendErrorAsync(session, envelope.RequestId,
                "An unexpected error occurred while invoking the tool.", "tool_error", cancellationToken);
        }
    }

    private static (string ToolName, JsonObject? Arguments) ParseInvocationPayload(JsonNode? payload)
    {
        if (payload is not JsonObject payloadObject)
        {
            throw new McpInvalidMessageException("Invoke message payload must be a JSON object.");
        }

        if (!payloadObject.TryGetPropertyValue("name", out var nameNode))
        {
            throw new McpInvalidMessageException("Invoke message is missing a 'name' property.");
        }

        string? toolName = null;
        try
        {
            toolName = nameNode?.GetValue<string>();
        }
        catch (InvalidOperationException)
        {
            // thrown when the node cannot be converted to string
        }

        if (string.IsNullOrWhiteSpace(toolName))
        {
            throw new McpInvalidMessageException("Invoke message 'name' property must be a non-empty string.");
        }

        JsonObject? arguments = null;
        if (payloadObject.TryGetPropertyValue("arguments", out var argumentsNode))
        {
            arguments = argumentsNode as JsonObject;
        }

        return (toolName!, arguments);
    }

    private static async Task SendErrorAsync(McpSession session, string? requestId, string message, string code, CancellationToken cancellationToken)
    {
        var payload = new JsonObject
        {
            ["code"] = code,
            ["message"] = message
        };

        await session.SendAsync(McpMessageTypes.Error, payload, requestId, cancellationToken);
    }
}
