public static class APIUtils
{
    // API parameters
    public static readonly string HostNameGeodeticDatum = NORCE.Drilling.GeodeticDatum.WebApp.Configuration.GeodeticDatumHostURL!;
    public static readonly string HostBasePathGeodeticDatum = "GeodeticDatum/api/";
    public static readonly HttpClient HttpClientGeodeticDatum = APIUtils.SetHttpClient(HostNameGeodeticDatum, HostBasePathGeodeticDatum);
    public static readonly NORCE.Drilling.GeodeticDatum.ModelShared.Client ClientGeodeticDatum = new NORCE.Drilling.GeodeticDatum.ModelShared.Client(APIUtils.HttpClientGeodeticDatum.BaseAddress!.ToString(), APIUtils.HttpClientGeodeticDatum);

    public static readonly string HostNameUnitConversion = NORCE.Drilling.GeodeticDatum.WebApp.Configuration.UnitConversionHostURL!;
    public static readonly string HostBasePathUnitConversion = "UnitConversion/api/";

    // API utility methods
    public static HttpClient SetHttpClient(string host, string microServiceUri)
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }; // temporary workaround for testing purposes: bypass certificate validation (not recommended for production environments due to security risks)
        HttpClient httpClient = new(handler)
        {
            BaseAddress = new Uri(host + microServiceUri)
        };
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        return httpClient;
    }
}