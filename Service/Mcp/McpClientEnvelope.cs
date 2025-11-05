using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp;

public sealed record McpClientEnvelope
{
    [JsonPropertyName("sessionId")]
    public string? SessionId { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("requestId")]
    public string? RequestId { get; init; }

    [JsonPropertyName("payload")]
    public JsonNode? Payload { get; init; }
}
