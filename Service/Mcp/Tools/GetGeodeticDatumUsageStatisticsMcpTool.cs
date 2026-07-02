using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NORCE.Drilling.GeodeticDatum.Service.Controllers;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp.Tools;

internal sealed class GetGeodeticDatumUsageStatisticsMcpTool : IMcpTool
{
    private readonly IServiceScopeFactory _scopeFactory;

    public GetGeodeticDatumUsageStatisticsMcpTool(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public string Name => "geodetic_datum_usage_statistics.get";

    public string Description => "Retrieve usage statistics for the GeodeticDatum microservice.";

    public JsonNode? InputSchema => null;

    public Task<JsonNode?> InvokeAsync(JsonObject? arguments, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var scope = _scopeFactory.CreateScope();
        var controller = scope.ServiceProvider.GetRequiredService<GeodeticDatumUsageStatisticsController>();
        var result = controller.GetGeodeticDatumUsageStatistics();
        var response = McpActionResultConverter.FromActionResult(result);
        return Task.FromResult<JsonNode?>(response);
    }
}
