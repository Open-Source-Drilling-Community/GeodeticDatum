using System.Text.Json.Nodes;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp.Tools;

internal static class McpToolResponses
{
    public static JsonNode CreateValidationError(string message)
    {
        return new JsonObject
        {
            ["status"] = 400,
            ["error"] = message
        };
    }

}
