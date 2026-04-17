using NORCE.Drilling.GeodeticDatum.WebPages;

namespace NORCE.Drilling.GeodeticDatum.WebApp;

public class WebPagesHostConfiguration : IGeodeticDatumWebPagesConfiguration
{
    public string GeodeticDatumHostURL { get; set; } = string.Empty;
    public string UnitConversionHostURL { get; set; } = string.Empty;
}
