using System.Text.Json.Nodes;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp;

public sealed record McpToolDescriptor(string Name, string Description, JsonNode? InputSchema);
