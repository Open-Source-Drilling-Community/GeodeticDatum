using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using GeodeticDatumModel = NORCE.Drilling.GeodeticDatum.Model.GeodeticDatum;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp.Tools;

internal sealed class PutGeodeticDatumByIdMcpTool : GeodeticDatumToolBase
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
            ["id"] = new JsonObject
            {
                ["type"] = "string",
                ["format"] = "uuid"
            },
            ["geodeticDatum"] = new JsonObject
            {
                ["type"] = "object"
            }
        },
        ["required"] = new JsonArray
        {
            "id",
            "geodeticDatum"
        },
        ["additionalProperties"] = false
    };

    public PutGeodeticDatumByIdMcpTool(IServiceScopeFactory scopeFactory)
        : base(scopeFactory)
    {
    }

    public override string Name => "geodetic_datum.update_by_id";

    public override string Description => "Update an existing geodetic datum identified by id.";

    public override JsonNode? InputSchema => Schema;

    public override Task<JsonNode?> InvokeAsync(JsonObject? arguments, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!McpToolArgumentHelpers.TryParseGuid(arguments, "id", out var id, out var idError))
        {
            return Task.FromResult<JsonNode?>(idError);
        }

        if (!TryDeserialize(arguments, out GeodeticDatumModel datum, out var datumError))
        {
            return Task.FromResult<JsonNode?>(datumError);
        }

        using var scope = CreateControllerScope();
        var result = scope.Controller.PutGeodeticDatumById(id, datum);
        var response = McpActionResultConverter.FromActionResult(result);
        return Task.FromResult<JsonNode?>(response);
    }

    private static bool TryDeserialize(JsonObject? arguments, out GeodeticDatumModel datum, out JsonNode? error)
    {
        datum = default!;
        error = null;

        if (arguments?["geodeticDatum"] is not JsonNode datumNode)
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
