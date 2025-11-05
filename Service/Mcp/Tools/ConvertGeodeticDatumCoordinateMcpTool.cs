using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NORCE.Drilling.GeodeticDatum.Service.Controllers;
using GeodeticDatumModel = NORCE.Drilling.GeodeticDatum.Model.GeodeticDatum;
using GeodeticConversionSetModel = NORCE.Drilling.GeodeticDatum.Model.GeodeticConversionSet;
using NORCE.Drilling.GeodeticDatum.Model;
using OSDC.DotnetLibraries.General.DataManagement;

namespace NORCE.Drilling.GeodeticDatum.Service.Mcp.Tools;

internal sealed class ConvertGeodeticDatumCoordinateMcpTool : GeodeticConversionSetToolBase
{
    private static readonly JsonObject Schema = new()
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["sourceDatumId"] = new JsonObject
            {
                ["type"] = "string",
                ["format"] = "uuid"
            },
            ["targetDatumId"] = new JsonObject
            {
                ["type"] = "string",
                ["format"] = "uuid"
            },
            ["latitude"] = new JsonObject
            {
                ["type"] = "number"
            },
            ["longitude"] = new JsonObject
            {
                ["type"] = "number"
            },
            ["verticalDepth"] = new JsonObject
            {
                ["type"] = "number"
            }
        },
        ["required"] = new JsonArray
        {
            "sourceDatumId",
            "targetDatumId",
            "latitude",
            "longitude",
            "verticalDepth"
        },
        ["additionalProperties"] = false
    };

    public ConvertGeodeticDatumCoordinateMcpTool(IServiceScopeFactory scopeFactory)
        : base(scopeFactory)
    {
    }

    public override string Name => "geodetic_datum.convert_coordinate";

    public override string Description => "Convert latitude, longitude, and vertical depth between geodetic datums using the microservice conversion pipeline.";

    public override JsonNode? InputSchema => Schema;

    public override Task<JsonNode?> InvokeAsync(JsonObject? arguments, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!McpToolArgumentHelpers.TryParseGuid(arguments, "sourceDatumId", out var sourceDatumId, out var error))
        {
            return Task.FromResult<JsonNode?>(error);
        }

        if (!McpToolArgumentHelpers.TryParseGuid(arguments, "targetDatumId", out var targetDatumId, out error))
        {
            return Task.FromResult<JsonNode?>(error);
        }

        if (!McpToolArgumentHelpers.TryParseDouble(arguments, "latitude", out var latitude, out error))
        {
            return Task.FromResult<JsonNode?>(error);
        }

        if (!McpToolArgumentHelpers.TryParseDouble(arguments, "longitude", out var longitude, out error))
        {
            return Task.FromResult<JsonNode?>(error);
        }

        if (!McpToolArgumentHelpers.TryParseDouble(arguments, "verticalDepth", out var verticalDepth, out error))
        {
            return Task.FromResult<JsonNode?>(error);
        }

        using var scope = CreateControllerScope();
        var conversionController = scope.Controller;
        var datumController = scope.Services.GetRequiredService<GeodeticDatumController>();

        if (!TryResolveDatum(datumController.GetGeodeticDatumById(sourceDatumId), out var sourceDatum, out var errorNode))
        {
            return Task.FromResult<JsonNode?>(errorNode);
        }

        if (!TryResolveDatum(datumController.GetGeodeticDatumById(targetDatumId), out var targetDatum, out errorNode))
        {
            return Task.FromResult<JsonNode?>(errorNode);
        }

        var createdSetIds = new List<Guid>();

        try
        {
            var sourceConversionSetId = Guid.NewGuid();
            var sourceConversionSet = CreateConversionSet(sourceConversionSetId, sourceDatum!, latitude, longitude, verticalDepth, isSource: true);

            var postSourceResult = conversionController.PostGeodeticConversionSet(sourceConversionSet);
            if (!IsSuccess(postSourceResult))
            {
                return Task.FromResult<JsonNode?>(McpActionResultConverter.FromActionResult(postSourceResult));
            }

            createdSetIds.Add(sourceConversionSetId);

            var sourceRetrieved = conversionController.GetGeodeticConversionSetById(sourceConversionSetId);
            if (!TryResolveConversionSet(sourceRetrieved, out var sourceConvertedSet, out errorNode))
            {
                return Task.FromResult<JsonNode?>(errorNode);
            }

            var sourceCoordinate = sourceConvertedSet!.GeodeticCoordinates.FirstOrDefault();
            if (sourceCoordinate?.LatitudeWGS84 is null || sourceCoordinate.LongitudeWGS84 is null || sourceCoordinate.VerticalDepthWGS84 is null)
            {
                return Task.FromResult<JsonNode?>(CreateError(500, "The conversion service did not return WGS84 coordinates."));
            }

            var targetConversionSetId = Guid.NewGuid();
            var targetConversionSet = CreateConversionSet(targetConversionSetId, targetDatum!, sourceCoordinate.LatitudeWGS84.Value, sourceCoordinate.LongitudeWGS84.Value, sourceCoordinate.VerticalDepthWGS84.Value, isSource: false);

            var postTargetResult = conversionController.PostGeodeticConversionSet(targetConversionSet);
            if (!IsSuccess(postTargetResult))
            {
                return Task.FromResult<JsonNode?>(McpActionResultConverter.FromActionResult(postTargetResult));
            }

            createdSetIds.Add(targetConversionSetId);

            var targetRetrieved = conversionController.GetGeodeticConversionSetById(targetConversionSetId);
            if (!TryResolveConversionSet(targetRetrieved, out var targetConvertedSet, out errorNode))
            {
                return Task.FromResult<JsonNode?>(errorNode);
            }

            var targetCoordinate = targetConvertedSet!.GeodeticCoordinates.FirstOrDefault();
            if (targetCoordinate?.LatitudeDatum is null || targetCoordinate.LongitudeDatum is null || targetCoordinate.VerticalDepthDatum is null)
            {
                return Task.FromResult<JsonNode?>(CreateError(500, "The target conversion service did not return datum coordinates."));
            }

            var payload = new JsonObject
            {
                ["status"] = 200,
                ["data"] = new JsonObject
                {
                    ["latitude"] = targetCoordinate.LatitudeDatum.Value,
                    ["longitude"] = targetCoordinate.LongitudeDatum.Value,
                    ["verticalDepth"] = targetCoordinate.VerticalDepthDatum.Value
                }
            };

            return Task.FromResult<JsonNode?>(payload);
        }
        finally
        {
            foreach (var id in createdSetIds)
            {
                try
                {
                    conversionController.DeleteGeodeticConversionSetById(id);
                }
                catch
                {
                }
            }
        }
    }

    private static bool TryResolveDatum(ActionResult<GeodeticDatumModel?> result, out GeodeticDatumModel? datum, out JsonNode? error)
    {
        if (result.Result is IActionResult actionResult && actionResult is not OkObjectResult)
        {
            error = McpActionResultConverter.FromActionResult(result);
            datum = null;
            return false;
        }

        datum = result.Value ?? (result.Result as OkObjectResult)?.Value as GeodeticDatumModel;
        if (datum is null)
        {
            error = CreateError(404, "The requested geodetic datum could not be found.");
            return false;
        }

        error = null;
        return true;
    }

    private static GeodeticConversionSetModel CreateConversionSet(Guid id, GeodeticDatumModel datum, double latitude, double longitude, double verticalDepth, bool isSource)
    {
        var meta = new MetaInfo
        {
            ID = id
        };

        var coordinate = new GeodeticCoordinate();
        if (isSource)
        {
            coordinate.LatitudeDatum = latitude;
            coordinate.LongitudeDatum = longitude;
            coordinate.VerticalDepthDatum = verticalDepth;
        }
        else
        {
            coordinate.LatitudeWGS84 = latitude;
            coordinate.LongitudeWGS84 = longitude;
            coordinate.VerticalDepthWGS84 = verticalDepth;
        }

        return new GeodeticConversionSetModel
        {
            MetaInfo = meta,
            GeodeticDatum = datum,
            GeodeticCoordinates = new List<GeodeticCoordinate> { coordinate }
        };
    }

    private static bool TryResolveConversionSet(ActionResult<GeodeticConversionSetModel?> result, out GeodeticConversionSetModel? conversionSet, out JsonNode? error)
    {
        if (result.Result is IActionResult actionResult && actionResult is not OkObjectResult)
        {
            error = McpActionResultConverter.FromActionResult(result);
            conversionSet = null;
            return false;
        }

        conversionSet = result.Value ?? (result.Result as OkObjectResult)?.Value as GeodeticConversionSetModel;
        if (conversionSet is null)
        {
            error = CreateError(500, "The conversion set could not be retrieved from the service.");
            return false;
        }

        error = null;
        return true;
    }

    private static bool IsSuccess(ActionResult result)
    {
        if (result is ObjectResult objectResult)
        {
            return objectResult.StatusCode is null or >= 200 and < 300;
        }

        if (result is StatusCodeResult statusResult)
        {
            return statusResult.StatusCode is >= 200 and < 300;
        }

        return true;
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
