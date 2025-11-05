using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Text.Json.Nodes;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp;

public sealed class McpSession : IAsyncDisposable
{
    private readonly Channel<McpServerMessage> _outbound;
    private long _lastActivityTicks;

    internal McpSession(string id, McpHandshake handshake, string transport)
    {
        Id = id;
        Handshake = handshake;
        Transport = transport;
        CreatedAtUtc = DateTimeOffset.UtcNow;
        _outbound = Channel.CreateUnbounded<McpServerMessage>(new UnboundedChannelOptions
        {
            AllowSynchronousContinuations = false,
            SingleReader = true,
            SingleWriter = false
        });
        Touch();
    }

    public string Id { get; }

    public McpHandshake Handshake { get; }

    public string Transport { get; }

    public DateTimeOffset CreatedAtUtc { get; }

    public DateTimeOffset LastActivityUtc
    {
        get
        {
            var ticks = Interlocked.Read(ref _lastActivityTicks);
            return new DateTimeOffset(ticks, TimeSpan.Zero);
        }
    }

    public void Touch()
    {
        var nowTicks = DateTimeOffset.UtcNow.UtcTicks;
        Interlocked.Exchange(ref _lastActivityTicks, nowTicks);
    }

    public ValueTask SendAsync(string type, JsonNode? payload, string? requestId, CancellationToken cancellationToken)
    {
        var message = new McpServerMessage(Id, type, payload, requestId);
        return _outbound.Writer.WriteAsync(message, cancellationToken);
    }

    public bool TrySend(string type, JsonNode? payload = null, string? requestId = null)
    {
        var message = new McpServerMessage(Id, type, payload, requestId);
        return _outbound.Writer.TryWrite(message);
    }

    public IAsyncEnumerable<McpServerMessage> ReadOutboundAsync(CancellationToken cancellationToken)
    {
        return _outbound.Reader.ReadAllAsync(cancellationToken);
    }

    public void Complete(Exception? error = null)
    {
        _outbound.Writer.TryComplete(error);
    }

    public ValueTask DisposeAsync()
    {
        Complete();
        return ValueTask.CompletedTask;
    }
}
