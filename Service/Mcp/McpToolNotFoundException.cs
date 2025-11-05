using System;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp;

public sealed class McpToolNotFoundException : Exception
{
    public McpToolNotFoundException(string toolName)
        : base($"No MCP tool registered with name '{toolName}'.")
    {
        ToolName = toolName;
    }

    public string ToolName { get; }
}
