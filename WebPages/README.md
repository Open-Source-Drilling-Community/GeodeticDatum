# NORCE.Drilling.GeodeticDatum.WebPages

Reusable Razor class library for the Geodetic Datum web UI.

It contains the `SpheroidMain`, `SpheroidEdit`, `GeodeticDatumMain`, `GeodeticDatumEdit`, `GeodeticConverter`, and `StatisticsGeodeticDatum` pages together with the supporting API and UI utility code they depend on.

## Package contents

- Spheroid list and edit pages
- Geodetic datum list and edit pages
- Geodetic conversion page
- Usage statistics page
- Host-configurable API access through injected configuration

## UI behavior

- The spheroid and geodetic datum edit pages use MudBlazor expansion panels to separate `Description` from `Configuration`.
- Description panels contain identifying metadata such as name, description, and related datum/spheroid choices.
- Configuration panels contain the unit-aware numerical inputs, such as spheroid axes and eccentricity parameters or geodetic datum translation/rotation parameters.
- Edit pages use `Save` and `Close` actions. Closing with unsaved edits opens a confirmation dialog before leaving the editor.
- The geodetic converter provides two left-aligned conversion sections: `WGS84 to Geodetic Datum` and `Geodetic Datum to WGS84`.

## Dependencies

- `OSDC.DotnetLibraries.Drilling.WebAppUtils`
- `MudBlazor`
- `OSDC.UnitConversion.DrillingRazorMudComponents`
- `ModelSharedOut`

## Host integration

The consuming app should:

1. Reference this package.
2. Provide an implementation of `IGeodeticDatumWebPagesConfiguration`.
3. Register that configuration and `IGeodeticDatumAPIUtils` in DI.
4. Add the `WebPages` assembly to the Blazor router `AdditionalAssemblies`.

## Required configuration

- `GeodeticDatumHostURL`
- `UnitConversionHostURL`
