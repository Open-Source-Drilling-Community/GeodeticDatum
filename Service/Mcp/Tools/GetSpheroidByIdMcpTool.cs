using System;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp.Tools;

internal sealed class GetSpheroidByIdMcpTool : SpheroidToolBase
{
    private static readonly JsonObject Schema = McpToolArgumentHelpers.CreateGuidSchema("id");

    public GetSpheroidByIdMcpTool(IServiceScopeFactory scopeFactory)
        : base(scopeFactory)
    {
    }

    public override string Name => "spheroid.get_by_id";

    public override string Description => "Retrieve a spheroid by identifier.";

    public override JsonNode? InputSchema => Schema;

    public override Task<JsonNode?> InvokeAsync(JsonObject? arguments, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!McpToolArgumentHelpers.TryParseGuid(arguments, "id", out var id, out var errorNode))
        {
            return Task.FromResult<JsonNode?>(errorNode);
        }

        using var scope = CreateControllerScope();
        var result = scope.Controller.GetSpheroidById(id);
        var response = McpActionResultConverter.FromActionResult(result);
        return Task.FromResult<JsonNode?>(response);
    }
}
