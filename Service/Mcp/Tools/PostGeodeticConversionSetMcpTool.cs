using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using GeodeticConversionSetModel = NORCE.Drilling.GeodeticDatum.Model.GeodeticConversionSet;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp.Tools;

internal sealed class PostGeodeticConversionSetMcpTool : GeodeticConversionSetToolBase
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
            ["geodeticConversionSet"] = new JsonObject
            {
                ["type"] = "object"
            }
        },
        ["required"] = new JsonArray
        {
            "geodeticConversionSet"
        },
        ["additionalProperties"] = false
    };

    public PostGeodeticConversionSetMcpTool(IServiceScopeFactory scopeFactory)
        : base(scopeFactory)
    {
    }

    public override string Name => "geodetic_conversion_set.create";

    public override string Description => "Create a geodetic conversion set using the GeodeticConversionSetController.";

    public override JsonNode? InputSchema => Schema;

    public override Task<JsonNode?> InvokeAsync(JsonObject? arguments, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!TryDeserialize(arguments, out GeodeticConversionSetModel entity, out var error))
        {
            return Task.FromResult<JsonNode?>(error);
        }

        using var scope = CreateControllerScope();
        var result = scope.Controller.PostGeodeticConversionSet(entity);
        var response = McpActionResultConverter.FromActionResult(result);
        return Task.FromResult<JsonNode?>(response);
    }

    private static bool TryDeserialize(JsonObject? arguments, out GeodeticConversionSetModel entity, out JsonNode? error)
    {
        entity = default!;
        error = null;

        if (arguments is null)
        {
            error = McpToolResponses.CreateValidationError("Arguments are required.");
            return false;
        }

        if (arguments["geodeticConversionSet"] is not JsonNode node)
        {
            error = McpToolResponses.CreateValidationError("Argument 'geodeticConversionSet' is required.");
            return false;
        }

        try
        {
            entity = node.Deserialize<GeodeticConversionSetModel>(DeserializerOptions) ?? throw new InvalidOperationException();
            return true;
        }
        catch (Exception ex) when (ex is JsonException or InvalidOperationException)
        {
            error = McpToolResponses.CreateValidationError("Argument 'geodeticConversionSet' could not be deserialized.");
            return false;
        }
    }
}
