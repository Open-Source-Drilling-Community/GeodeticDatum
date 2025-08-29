# Model — Geodetic Datum Core Library

The Model project provides the core domain types and algorithms for working with geodetic datums, spheroids, coordinates, and octree encoding/decoding in the GeodeticDatum solution. It encapsulates the math for converting coordinates between regional datums and the global WGS84 reference, plus helpers for geocentric/cartesian conversions and proj4-like spheroid descriptions.

## Purpose in the Solution

- Defines shared domain types used by the API and UI:
  - Spheroids (e.g., WGS84, Clarke 1880, International 1924 …)
  - Geodetic datums (e.g., WGS84, ED50 regional variants …)
  - Geodetic coordinates with conversion utilities
  - Batch conversion set with octree encoding
- Implements datum transformations (Molodensky, Helmert) and conversions to/from geocentric and cartesian coordinates.
- Serves as the single source of truth for geodetic math used by the microservice.

Project references:
- The Service project references this library to expose REST endpoints using these types and algorithms.
- ModelSharedOut consumes the API schema and generates a shared C# model + merged OpenAPI used by clients (WebApp, tests).

## Key Concepts & Conventions

- Angles are in radians.
- Lengths and elevations are in meters.
- VerticalDepth is the opposite of elevation (TVD vs elevation): `VerticalDepth = -Elevation`.
- When a datum is exactly WGS84 (zero translations/rotations and scale 1 with a WGS84 spheroid), conversions are pass-through.

## Main Types

- Spheroid: definition and completion of ellipsoid parameters from 2+ inputs (semi-axes, flattening, eccentricity).
- GeodeticDatum: ties a spheroid with translations, rotations, and scale; includes built-ins (WGS84, ED50 variants, Adindan …).
- GeodeticCoordinate: coordinate container with conversion methods across datums, geocentric, and cartesian spaces.
- GeodeticConversionSet: batch conversion utility that can also compute octree codes at configurable depth.
- Light-weight DTOs: GeodeticDatumLight, GeodeticConversionSetLight for listing/indexing scenarios.

## Install

Reference the project from other projects in the solution:

```xml
<ProjectReference Include="..\Model\Model.csproj" />
```

Build with:

```bash
dotnet build
```

## Usage Examples

### 1) Convert coordinates from a regional datum to WGS84

```csharp
using NORCE.Drilling.GeodeticDatum.Model;

// Input in the reference datum (angles in radians, depths in meters)
var coord = new GeodeticCoordinate
{
    LatitudeDatum = 1.01229097,      // ≈ 58° in radians
    LongitudeDatum = 0.13962634,     // ≈ 8° in radians
    VerticalDepthDatum = 100.0       // TVD = 100 m (Elevation = -100 m)
};

// Choose a reference datum (built-ins available)
var datum = GeodeticDatum.ED50NorwayFinland;

// Compute WGS84
coord.ToFundamental(datum);

// Result in WGS84
double? latWgs = coord.LatitudeWGS84;
double? lonWgs = coord.LongitudeWGS84;
double? tvdWgs = coord.VerticalDepthWGS84;   // still TVD (negative of elevation)
```

### 2) Convert from WGS84 to a target datum

```csharp
using NORCE.Drilling.GeodeticDatum.Model;

var datum = GeodeticDatum.ED50UK!;

// Inputs are WGS84 latitude/longitude (radians) and elevation (meters)
double latWgs = 1.01229097;   // ≈ 58°
double lonWgs = 0.13962634;   // ≈ 8°
double elevWgs = 50.0;        // Elevation = +50 m

GeodeticCoordinate.FromFundamental(
    datum,
    latWgs,
    lonWgs,
    elevWgs,
    out double latDatum,
    out double lonDatum,
    out double elevDatum);

// If you need TVD in the target datum:
double tvdDatum = -elevDatum;
```

### 3) Geocentric and cartesian conversions

```csharp
using NORCE.Drilling.GeodeticDatum.Model;

var datum = GeodeticDatum.WGS84;  // spheroid information is taken from the datum

// To geocentric (angles in radians, elevation in meters)
var gc = new GeodeticCoordinate();
gc.ToGeocentric(datum, geodeticLatitude: 1.0, geodeticLongitude: 0.5, geodeticElevation: 10.0,
                out double latGc, out double lonGc, out double rGc);

// To cartesian (ECEF-like)
GeodeticCoordinate.ToCartesian(
    spheroid: Spheroid.WGS84,
    latitude: 1.0,
    longitude: 0.5,
    elevation: 10.0,
    out double x, out double y, out double z);

// Back from cartesian
GeodeticCoordinate.FromCartesian(
    spheroid: Spheroid.WGS84,
    x, y, z,
    out double lat, out double lon, out double elev);
```

### 4) Batch conversion + octree encoding

```csharp
using NORCE.Drilling.GeodeticDatum.Model;

var set = new GeodeticConversionSet
{
    GeodeticDatum = GeodeticDatum.WGS84
};

set.GeodeticCoordinates.Add(new GeodeticCoordinate
{
    LatitudeWGS84 = 1.01229097,
    LongitudeWGS84 = 0.13962634,
    VerticalDepthWGS84 = 120.0,  // TVD
    OctreeDepth = 24
});

bool ok = set.Calculate();
var first = set.GeodeticCoordinates[0];
var octree = first.OctreeCode; // encoded position at the requested depth
```

### 5) Build a proj4-like spheroid string

```csharp
string proj4 = GeodeticCoordinate.GetProj4String(Spheroid.WGS84);
// e.g. "+a=6378137 +b=6356752.314245… +f=… +rf=… +e=… +es=…"
```

## Built-in Catalog

Use predefined spheroids and datums when available instead of re-authoring parameters. Examples:

- Spheroids: `Spheroid.WGS84`, `Spheroid.International_1924`, `Spheroid.Clarke_1880`, `Spheroid.NAD83`, …
- Datums: `GeodeticDatum.WGS84`, `GeodeticDatum.ED50NorwayFinland`, `GeodeticDatum.ED50UK`, `GeodeticDatum.AdindanBurkinaFaso`, …

Each built-in contains sensible translations/rotations/scale where applicable.

## Integration With the Solution

- Service: hosts the HTTP API and references this project to perform conversions and data access.
  - Project reference: `Service/Service.csproj`
  - Example usage of Model types: `Service/Managers/SpheroidManager.cs`
- ModelSharedOut: generates a merged OpenAPI document and a shared C# client/model for consumers.
  - Writes OpenAPI bundle to `Service/wwwroot/json-schema/GeodeticDatumMergedModel.json`
  - Generates a C# shared model `ModelSharedOut/GeodeticDatumMergedModel.cs`
- WebApp: consumes the generated shared model to interact with the API using types compatible with Model.
- ModelTest: place for unit tests over Model algorithms and types.

## Notes & Tips

- Prefer radians for all angular inputs/outputs; convert degrees using `Math.PI/180` when needed.
- When only translations are specified for a datum (no rotations/scale), the Molodensky transformation is used.
- When translations + rotations + scale are specified, the Helmert transformation is used.
- Spheroid.Calculate() computes missing parameters when at least 2 valid parameters are provided (and at least one semi-axis).

## License & Contributions

This project is part of the NORCE Drilling and Wells GeodeticDatum solution. Contributions should follow the solution’s guidelines and coding style.

