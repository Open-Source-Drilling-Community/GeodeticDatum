using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp.Tools;

internal sealed class GetAllSpheroidMetaInfoMcpTool : SpheroidToolBase
{
    public GetAllSpheroidMetaInfoMcpTool(IServiceScopeFactory scopeFactory)
        : base(scopeFactory)
    {
    }

    public override string Name => "spheroid.get_all_meta";

    public override string Description => "Retrieve metadata for all spheroids.";

    public override JsonNode? InputSchema => null;

    public override Task<JsonNode?> InvokeAsync(JsonObject? arguments, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var scope = CreateControllerScope();
        var result = scope.Controller.GetAllSpheroidMetaInfo();
        var response = McpActionResultConverter.FromActionResult(result);
        return Task.FromResult<JsonNode?>(response);
    }
}
