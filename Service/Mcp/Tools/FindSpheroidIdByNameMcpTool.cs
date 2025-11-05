using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NORCE.Drilling.GeodeticDatum.Model;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp.Tools;

internal sealed class FindSpheroidIdByNameMcpTool : SpheroidToolBase
{
    private static readonly JsonObject Schema = new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["name"] = new JsonObject
            {
                ["type"] = "string",
                ["minLength"] = 1
            }
        },
        ["required"] = new JsonArray
        {
            "name"
        },
        ["additionalProperties"] = false
    };

    public FindSpheroidIdByNameMcpTool(IServiceScopeFactory scopeFactory)
        : base(scopeFactory)
    {
    }

    public override string Name => "spheroid.find_id_by_name";

    public override string Description => "Look up a spheroid identifier by a case and whitespace insensitive name match.";

    public override JsonNode? InputSchema => Schema;

    public override Task<JsonNode?> InvokeAsync(JsonObject? arguments, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!McpNameMatching.TryReadRequiredString(arguments, "name", out var name, out var error))
        {
            return Task.FromResult<JsonNode?>(error);
        }

        using var scope = CreateControllerScope();
        var response = scope.Controller.GetAllSpheroid();

        if (response.Result is IActionResult actionResult && actionResult is not OkObjectResult)
        {
            return Task.FromResult<JsonNode?>(McpActionResultConverter.FromActionResult(response));
        }

        var spheroids = response.Value ?? (response.Result as OkObjectResult)?.Value as IEnumerable<Spheroid?>;
        if (spheroids is null)
        {
            return Task.FromResult<JsonNode?>(CreateError(500, "Unable to retrieve spheroids."));
        }

        var normalizedTarget = McpNameMatching.Normalize(name);
        var match = spheroids.FirstOrDefault(s => Matches(normalizedTarget, s));

        if (match?.MetaInfo?.ID is Guid id && id != Guid.Empty)
        {
            var payload = new JsonObject
            {
                ["status"] = 200,
                ["data"] = new JsonObject
                {
                    ["id"] = id.ToString(),
                    ["name"] = match.Name
                }
            };

            return Task.FromResult<JsonNode?>(payload);
        }

        return Task.FromResult<JsonNode?>(CreateError(404, "No spheroid matching the provided name was found."));
    }

    private static bool Matches(string normalizedTarget, Spheroid? candidate)
    {
        return candidate is not null &&
               !string.IsNullOrEmpty(candidate.Name) &&
               McpNameMatching.Normalize(candidate.Name) == normalizedTarget;
    }

    private static JsonObject CreateError(int status, string message)
    {
        return new JsonObject
        {
            ["status"] = status,
            ["error"] = message
        };
    }
}
