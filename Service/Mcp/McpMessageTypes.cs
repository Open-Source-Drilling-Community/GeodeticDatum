namespace NORCE.Drilling.GeodeticDatum.Service.Mcp;

internal static class McpMessageTypes
{
    public const string SessionCreated = "session.created";
    public const string ToolsList = "tools.list";
    public const string Ping = "ping";
    public const string Pong = "pong";
    public const string InvokeTool = "invoke";
    public const string ToolResult = "tool.result";
    public const string Error = "error";
}
