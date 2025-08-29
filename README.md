# GeodeticDatum — Solution Overview

A .NET 8 solution providing geodetic datum computations and management via a REST API and a Blazor Server web UI. Core geodetic math (datum transforms, geocentric/cartesian conversions, octree encoding) lives in a shared Model library. A generator project produces a merged OpenAPI bundle and a strongly-typed C# client for consumers.

**Live Example**
- Service (Swagger): https://dev.DigiWells.no/GeodeticDatum/api/swagger
- WebApp (UI): https://dev.DigiWells.no/GeodeticDatum/webapp

## Projects

- Model: core types and algorithms (spheroids, datums, conversions, octree).
- Service: ASP.NET Core API exposing CRUD and conversion endpoints; persists to SQLite under `home/`.
- ModelSharedOut: generator that merges OpenAPI inputs and produces a C# client + merged JSON.
- WebApp: Blazor Server UI that consumes the Service using the generated client.
- ModelTest: NUnit tests for Model algorithms and built-ins.
- ServiceTest: NUnit tests hitting the running Service via the generated client.
- home/: local data folder (SQLite DB `home/GeodeticDatum.db`, usage history `home/history.json`).

## Installation

- Prerequisites: .NET 8 SDK, optional Docker.
- Build solution: `dotnet build`
- Generate shared client + merged OpenAPI: `dotnet run --project ModelSharedOut/ModelSharedOut.csproj`
- Run Service: `dotnet run --project Service/Service.csproj`
- Run WebApp: `dotnet run --project WebApp/WebApp.csproj`

Notes
- Service base path: `/GeodeticDatum/api`; Swagger UI served at `/GeodeticDatum/api/swagger`.
- WebApp base path: `/GeodeticDatum/webapp`.
- Service writes SQLite DB and stats under `home/` at repo root. Mount this folder when containerizing.

## Usage Examples

- List geodetic datums (REST): `GET /GeodeticDatum/api/GeodeticDatum`
- Create spheroid (REST):
  ```bash
  curl -X POST \
    http://localhost:8080/GeodeticDatum/api/Spheroid \
    -H "Content-Type: application/json" \
    -d '{
          "MetaInfo": { "ID": "00000000-0000-0000-0000-000000000001" },
          "Name": "Custom WGS84",
          "SemiMajorAxis": { "ScalarValue": 6378137 },
          "InverseFlattening": { "ScalarValue": 298.257223563 }
        }'
  ```
- Convert WGS84 → datum via batch conversion (UI): navigate to `/GeodeticConverter` in the WebApp.
- Programmatic client (C#):
  ```csharp
  using NORCE.Drilling.GeodeticDatum.ModelShared;
  var http = new HttpClient { BaseAddress = new Uri("http://localhost:8080/GeodeticDatum/api/") };
  var client = new Client(http.BaseAddress!.ToString(), http);
  var datums = await client.GetAllGeodeticDatumAsync();
  ```

## Dependencies

- Runtime: .NET 8, ASP.NET Core, SQLite (`Microsoft.Data.Sqlite`).
- API & Docs: `Microsoft.OpenApi.*`, `Swashbuckle.AspNetCore.*` (Swagger UI).
- Client generation: `NSwag.CodeGeneration.CSharp`, `NJsonSchema.*`, `Microsoft.OpenApi.Readers`.
- UI: `MudBlazor`, `OSDC.UnitConversion.DrillingRazorMudComponents`.
- Domain libs: `OSDC.DotnetLibraries.*`.

## Security & Deployment

- This sample stack stores data in a local SQLite database. No auth is configured by default.
- Docker images are available under the DigiWells organization on Docker Hub.
- Deployment (Kubernetes/Helm) and further docs: https://github.com/NORCE-DrillingAndWells/DrillingAndWells/wiki

## Funding

The current work has been funded by the Research Council of Norway and Industry partners in the SFI DigiWells (2020–2028) program.

## Contributors

Eric Cayeux — NORCE Energy Modelling and Automation

Gilles Pelfrene — NORCE Energy Modelling and Automation

Andrew Holsaeter — NORCE Energy Modelling and Automation

Lucas Volpi — NORCE Energy Modelling and Automation
