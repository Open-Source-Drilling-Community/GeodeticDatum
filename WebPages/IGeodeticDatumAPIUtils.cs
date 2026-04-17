using NORCE.Drilling.GeodeticDatum.ModelShared;

namespace NORCE.Drilling.GeodeticDatum.WebPages;

public interface IGeodeticDatumAPIUtils
{
    string HostNameGeodeticDatum { get; }
    string HostBasePathGeodeticDatum { get; }
    HttpClient HttpClientGeodeticDatum { get; }
    Client ClientGeodeticDatum { get; }

    string HostNameUnitConversion { get; }
    string HostBasePathUnitConversion { get; }
}
