public static class DataUtils
{
    // default values
    public const double DEFAULT_VALUE = 999.25;
    public static string DEFAULT_NAME_GeodeticDatum = "Default GeodeticDatum Name";
    public static string DEFAULT_DESCR_GeodeticDatum = "Default GeodeticDatum Description";
    public static string DEFAULT_NAME_Spheroid = "Default Spheroid Name";
    public static string DEFAULT_DESCR_Spheroid = "Default Spheroid Description";
    public static string DEFAULT_NAME_ConversionSet = "Default Conversion Set Name";
    public static string DEFAULT_DESCR_ConversionSet = "Default Conversion Set Description";

    // unit management
    public static class UnitAndReferenceParameters
    {
        public static string? UnitSystemName { get; set; } = "Metric";
        public static string? DepthReferenceName { get; set; }
        public static string? PositionReferenceName { get; set; }
        public static string? AzimuthReferenceName { get; set; }
        public static string? PressureReferenceName { get; set; }
        public static string? DateReferenceName { get; set; }
    }

    public static void UpdateUnitSystemName(string val)
    {
        UnitAndReferenceParameters.UnitSystemName = (string)val;
    }

    // units and labels
    public static readonly string GeodeticDatumNameLabel = "Name";
    public static readonly string GeodeticDatumDescrLabel = "Description";
    public static readonly string SpheroidNameLabel = "Name";
    public static readonly string SpheroidDescrLabel = "Description";

}