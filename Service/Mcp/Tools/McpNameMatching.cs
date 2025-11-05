using System;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp.Tools;

internal static class McpNameMatching
{
    public static bool TryReadRequiredString(JsonObject? arguments, string propertyName, out string value, out JsonNode? error)
    {
        value = string.Empty;
        error = null;

        var node = arguments?[propertyName];
        var text = node?.GetValue<string>();
        if (string.IsNullOrWhiteSpace(text))
        {
            error = McpToolResponses.CreateValidationError($"Argument '{propertyName}' is required.");
            return false;
        }

        value = text;
        return true;
    }

    public static string Normalize(string value)
    {
        if (value is null)
        {
            return string.Empty;
        }

        var trimmed = value.Trim();
        if (trimmed.Length == 0)
        {
            return string.Empty;
        }

        return Regex.Replace(trimmed, "\\s+", " ").ToLowerInvariant();
    }
}
