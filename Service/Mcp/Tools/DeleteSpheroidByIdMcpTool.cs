using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp.Tools;

internal sealed class DeleteSpheroidByIdMcpTool : SpheroidToolBase
{
    private static readonly JsonObject Schema = McpToolArgumentHelpers.CreateGuidSchema("id");

    public DeleteSpheroidByIdMcpTool(IServiceScopeFactory scopeFactory)
        : base(scopeFactory)
    {
    }

    public override string Name => "spheroid.delete_by_id";

    public override string Description => "Delete a spheroid identified by id.";

    public override JsonNode? InputSchema => Schema;

    public override Task<JsonNode?> InvokeAsync(JsonObject? arguments, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!McpToolArgumentHelpers.TryParseGuid(arguments, "id", out var id, out var error))
        {
            return Task.FromResult<JsonNode?>(error);
        }

        using var scope = CreateControllerScope();
        var result = scope.Controller.DeleteSpheroidById(id);
        var response = McpActionResultConverter.FromActionResult(result);
        return Task.FromResult<JsonNode?>(response);
    }
}
