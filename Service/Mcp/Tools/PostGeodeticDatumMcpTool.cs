using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using GeodeticDatumModel = NORCE.Drilling.GeodeticDatum.Model.GeodeticDatum;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp.Tools;

internal sealed class PostGeodeticDatumMcpTool : GeodeticDatumToolBase
{
    private static readonly JsonSerializerOptions DeserializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private static readonly JsonObject Schema = new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["geodeticDatum"] = new JsonObject
            {
                ["type"] = "object"
            }
        },
        ["required"] = new JsonArray
        {
            "geodeticDatum"
        },
        ["additionalProperties"] = false
    };

    public PostGeodeticDatumMcpTool(IServiceScopeFactory scopeFactory)
        : base(scopeFactory)
    {
    }

    public override string Name => "geodetic_datum.create";

    public override string Description => "Create a geodetic datum using the GeodeticDatumController.";

    public override JsonNode? InputSchema => Schema;

    public override Task<JsonNode?> InvokeAsync(JsonObject? arguments, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!TryDeserialize(arguments, out GeodeticDatumModel datum, out var error))
        {
            return Task.FromResult<JsonNode?>(error);
        }

        using var scope = CreateControllerScope();
        var result = scope.Controller.PostGeodeticDatum(datum);
        var response = McpActionResultConverter.FromActionResult(result);
        return Task.FromResult<JsonNode?>(response);
    }

    private static bool TryDeserialize(JsonObject? arguments, out GeodeticDatumModel datum, out JsonNode? error)
    {
        datum = default!;
        error = null;

        if (arguments is null)
        {
            error = McpToolResponses.CreateValidationError("Arguments are required.");
            return false;
        }

        if (arguments["geodeticDatum"] is not JsonNode datumNode)
        {
            error = McpToolResponses.CreateValidationError("Argument 'geodeticDatum' is required.");
            return false;
        }

        try
        {
            datum = datumNode.Deserialize<GeodeticDatumModel>(DeserializerOptions) ?? throw new InvalidOperationException();
            return true;
        }
        catch (Exception ex) when (ex is JsonException or InvalidOperationException)
        {
            error = McpToolResponses.CreateValidationError("Argument 'geodeticDatum' could not be deserialized.");
            return false;
        }
    }
}
