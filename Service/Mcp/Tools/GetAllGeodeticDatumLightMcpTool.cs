using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp.Tools;

internal sealed class GetAllGeodeticDatumLightMcpTool : GeodeticDatumToolBase
{
    public GetAllGeodeticDatumLightMcpTool(IServiceScopeFactory scopeFactory)
        : base(scopeFactory)
    {
    }

    public override string Name => "geodetic_datum.get_all_light";

    public override string Description => "Retrieve all geodetic datum light records.";

    public override JsonNode? InputSchema => null;

    public override Task<JsonNode?> InvokeAsync(JsonObject? arguments, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var scope = CreateControllerScope();
        var result = scope.Controller.GetAllGeodeticDatumLight();
        var response = McpActionResultConverter.FromActionResult(result);
        return Task.FromResult<JsonNode?>(response);
    }
}
