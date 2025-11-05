using System.Reflection;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp;

internal static class McpServerConstants
{
    public const string ServerName = "GeodeticDatum MCP Server";

    public static string ServerVersion { get; } =
        Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0";
}
