using System.Text.Json.Nodes;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp;

public sealed record McpServerMessage(string SessionId, string Type, JsonNode? Payload, string? RequestId);
