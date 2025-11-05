using System;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp;

public sealed class McpInvalidMessageException : Exception
{
    public McpInvalidMessageException(string message)
        : base(message)
    {
    }
}
