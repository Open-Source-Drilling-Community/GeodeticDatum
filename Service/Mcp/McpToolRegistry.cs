using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp;

public class McpToolRegistry
{
    private readonly ConcurrentDictionary<string, IMcpTool> _tools;
    private readonly ILogger<McpToolRegistry> _logger;

    public McpToolRegistry(IEnumerable<IMcpTool> tools, ILogger<McpToolRegistry> logger)
    {
        _tools = new ConcurrentDictionary<string, IMcpTool>(StringComparer.OrdinalIgnoreCase);
        _logger = logger;

        foreach (var tool in tools)
        {
            RegisterTool(tool);
        }
    }

    public IReadOnlyList<McpToolDescriptor> DescribeTools()
    {
        return _tools.Values
            .Select(tool => new McpToolDescriptor(tool.Name, tool.Description, tool.InputSchema))
            .OrderBy(tool => tool.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    public void RegisterTool(IMcpTool tool)
    {
        if (string.IsNullOrWhiteSpace(tool.Name))
        {
            throw new ArgumentException("Tool name must be defined.", nameof(tool));
        }

        _tools[tool.Name] = tool;
        _logger.LogInformation("Registered MCP tool {ToolName}", tool.Name);
    }

    public bool TryGetTool(string toolName, out IMcpTool? tool)
    {
        return _tools.TryGetValue(toolName, out tool);
    }

    public async Task<JsonNode?> InvokeAsync(string toolName, JsonObject? arguments, CancellationToken cancellationToken)
    {
        if (!_tools.TryGetValue(toolName, out var tool))
        {
            throw new McpToolNotFoundException(toolName);
        }

        return await tool.InvokeAsync(arguments, cancellationToken);
    }
}
