# ModelSharedOut — Shared Client & OpenAPI Bundle

Generates a shared C# client/model and a merged OpenAPI document for the GeodeticDatum solution. It reads OpenAPI JSON inputs from `ModelSharedOut/json-schemas`, normalizes schema IDs to short names, merges paths and schemas, then:

- Writes a merged OpenAPI bundle to `Service/wwwroot/json-schema/GeodeticDatumMergedModel.json` (served by Swagger UI).
- Generates a C# client/model file at `ModelSharedOut/GeodeticDatumMergedModel.cs` (namespace `NORCE.Drilling.GeodeticDatum.ModelShared`).

## Purpose in the Solution

- Centralizes and shares API contracts for clients (WebApp, tests, tools) via generated C# types and a strongly-typed client.
- Produces a single, merged OpenAPI JSON for the Service to expose (`/GeodeticDatum/api/swagger/merged/swagger.json`).
- Keeps UI and clients in sync with Service capabilities and model types.

## Installation

Prerequisites:
- .NET 8 SDK

Project reference (for consumers):

```xml
<ProjectReference Include="..\ModelSharedOut\ModelSharedOut.csproj" />
```

Inputs folder layout:
- Place dependency OpenAPI JSON files under `ModelSharedOut/json-schemas/`.
- In Debug builds, the Service produces `ModelSharedOut/json-schemas/GeodeticDatumFullName.json` automatically (see `Service/Service.csproj` target `CreateSwaggerJson`).

## Usage

1) Prepare input schemas
- Ensure `ModelSharedOut/json-schemas/*.json` contains the OpenAPI of Service and any external dependencies you want to include.

2) Generate bundle and client
- From repo root:

```bash
# build and run the generator
dotnet run --project ModelSharedOut/ModelSharedOut.csproj
```

3) Outputs
- `Service/wwwroot/json-schema/GeodeticDatumMergedModel.json` — merged OpenAPI (served by Swagger UI in Service).
- `ModelSharedOut/GeodeticDatumMergedModel.cs` — generated C# client and DTOs (namespace `NORCE.Drilling.GeodeticDatum.ModelShared`).

4) Use the generated client (example)

```csharp
using NORCE.Drilling.GeodeticDatum.ModelShared; // generated types

var http = new HttpClient { BaseAddress = new Uri("https://dev.digiwells.no/GeodeticDatum/api/") };
var client = new Client(http.BaseAddress!.ToString(), http);

// List datums
var datums = await client.GetAllGeodeticDatumAsync();

// Create a conversion set
var set = new GeodeticConversionSet
{
    MetaInfo = new MetaInfo { ID = Guid.NewGuid() },
    GeodeticDatum = datums.FirstOrDefault(),
    GeodeticCoordinates = new List<GeodeticCoordinate>
    {
        new GeodeticCoordinate { LatitudeWGS84 = 1.0, LongitudeWGS84 = 0.5, VerticalDepthWGS84 = 100.0 }
    }
};
await client.PostGeodeticConversionSetAsync(set);
```

## How It Works

- Reads all `*.json` under `json-schemas/` and parses them using `Microsoft.OpenApi.Readers`.
- Merges Paths and normalizes/merges Schemas via `OpenApiSchemaReferenceUpdater`:
  - Strips namespaces from schema IDs (short type names) and updates `$ref` accordingly.
  - Deduplicates and resolves references across inputs.
- Serializes to JSON (`OpenAPI 3.0.3` forced from `3.0.4` for Swagger UI compatibility).
- Generates C# client and DTOs using `NSwag` + `NJsonSchema` with `System.Text.Json`.

## Dependencies

NuGet (selected):
- `Microsoft.OpenApi` / `Microsoft.OpenApi.Readers` — parse and compose OpenAPI docs.
- `NSwag.CodeGeneration.CSharp` / `NJsonSchema.*` — C# client/DTO generation.
- `Namotion.Reflection` — reflection helpers used by generators.

Internal:
- Outputs are consumed by:
  - Service: serves the merged JSON via Swagger UI (see `Service/Program.cs` and `SwaggerMiddlewareExtensions`).
  - WebApp: references this project and uses the generated `Client` in `Shared/APIUtils.cs`.

## Integration With the Solution

- Service
  - Debug builds run `CreateSwaggerJson` to emit an input schema for merging at `ModelSharedOut/json-schemas/GeodeticDatumFullName.json`.
  - Serves `Service/wwwroot/json-schema/GeodeticDatumMergedModel.json` at `/GeodeticDatum/api/swagger/merged/swagger.json`.
- WebApp
  - Uses `NORCE.Drilling.GeodeticDatum.ModelShared.Client` for all API calls.
- Model
  - Core domain and math used by Service; types surface through the generated DTOs.

## Tips

- Re-run the generator whenever Service controllers or models change to keep the shared client/types and merged JSON in sync.
- If adding external dependencies, drop their OpenAPI JSON into `json-schemas/` before running.
- The generator overwrites `GeodeticDatumMergedModel.cs` and the merged JSON; commit results as appropriate for your workflow.

