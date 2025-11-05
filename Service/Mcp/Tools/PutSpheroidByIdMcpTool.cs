using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NORCE.Drilling.GeodeticDatum.Model;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp.Tools;

internal sealed class PutSpheroidByIdMcpTool : SpheroidToolBase
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
            ["spheroid"] = new JsonObject
            {
                ["type"] = "object"
            }
        },
        ["required"] = new JsonArray
        {
            "id",
            "spheroid"
        },
        ["additionalProperties"] = false
    };

    public PutSpheroidByIdMcpTool(IServiceScopeFactory scopeFactory)
        : base(scopeFactory)
    {
    }

    public override string Name => "spheroid.update_by_id";

    public override string Description => "Update an existing spheroid identified by id.";

    public override JsonNode? InputSchema => Schema;

    public override Task<JsonNode?> InvokeAsync(JsonObject? arguments, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!McpToolArgumentHelpers.TryParseGuid(arguments, "id", out var id, out var idError))
        {
            return Task.FromResult<JsonNode?>(idError);
        }

        if (!TryDeserialize(arguments, out var spheroid, out var spheroidError))
        {
            return Task.FromResult<JsonNode?>(spheroidError);
        }

        using var scope = CreateControllerScope();
        var result = scope.Controller.PutSpheroidById(id, spheroid);
        var response = McpActionResultConverter.FromActionResult(result);
        return Task.FromResult<JsonNode?>(response);
    }

    private static bool TryDeserialize(JsonObject? arguments, out Spheroid spheroid, out JsonNode? error)
    {
        spheroid = default!;
        error = null;

        if (arguments?["spheroid"] is not JsonNode node)
        {
            error = McpToolResponses.CreateValidationError("Argument 'spheroid' is required.");
            return false;
        }

        try
        {
            spheroid = node.Deserialize<Spheroid>(DeserializerOptions) ?? throw new InvalidOperationException();
            return true;
        }
        catch (Exception ex) when (ex is JsonException or InvalidOperationException)
        {
            error = McpToolResponses.CreateValidationError("Argument 'spheroid' could not be deserialized.");
            return false;
        }
    }
}
