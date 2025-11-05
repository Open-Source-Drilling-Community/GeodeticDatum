using System;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp;

public sealed class McpSessionNotFoundException : Exception
{
    public McpSessionNotFoundException(string sessionId)
        : base($"No MCP session with id '{sessionId}' was found.")
    {
        SessionId = sessionId;
    }

    public string SessionId { get; }
}
