using System.Net.Http.Headers;
using NORCE.Drilling.GeodeticDatum.ModelShared;

namespace ServiceTest
{
    public class GeodeticConversionSetApiTests
    {
        //private static string host = "https://localhost:5001/";
        //private static string host = "http://localhost:5002/";
        private static string host = "http://localhost:8080/";
        //private static string host = "https://dev.DigiWells.no/";
        //private static string host = "https://app.DigiWells.no/";
        private static HttpClient httpClient = null!;
        private static Client client = null!;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(host + "GeodeticDatum/api/")
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client = new Client(httpClient.BaseAddress.ToString(), httpClient);
        }

        [Test]
        public async Task ConversionSet_Post_Get_Delete_Works()
        {
            // Pick WGS84 datum from service
            var datums = (List<GeodeticDatum>)await client.GetAllGeodeticDatumAsync();
            var wgs84 = datums.FirstOrDefault(d => string.Equals(d.Name, "WGS84", StringComparison.OrdinalIgnoreCase));
            Assert.That(wgs84, Is.Not.Null, "WGS84 datum should exist in service");

            var id = Guid.NewGuid();
            var set = new GeodeticConversionSet
            {
                MetaInfo = new MetaInfo { ID = id },
                Name = "API Test Conversion",
                Description = "Created by API test",
                GeodeticDatum = wgs84,
                GeodeticCoordinates = new List<GeodeticCoordinate>
                {
                    new GeodeticCoordinate
                    {
                        LatitudeWGS84 = 0.5,
                        LongitudeWGS84 = 1.0,
                        VerticalDepthWGS84 = 150.0,
                        OctreeDepth = 8
                    }
                }
            };

            // POST
            try
            {
                await client.PostGeodeticConversionSetAsync(set);
            }
            catch (ApiException ex)
            {
                Assert.Fail($"POST GeodeticConversionSet failed: {ex.Message}");
            }

            // GET by id
            GeodeticConversionSet? fetched = null;
            try
            {
                fetched = await client.GetGeodeticConversionSetByIdAsync(id);
            }
            catch (ApiException ex)
            {
                Assert.Fail($"GET GeodeticConversionSet by id failed: {ex.Message}");
            }
            Assert.That(fetched, Is.Not.Null);
            Assert.That(fetched!.GeodeticCoordinates, Is.Not.Null.And.Not.Empty);
            var c = fetched!.GeodeticCoordinates.First();
            // With WGS84 datum and WGS84 inputs, the service should echo coordinates (identity path)
            Assert.That(c.LatitudeWGS84, Is.EqualTo(0.5).Within(1e-12));
            Assert.That(c.LongitudeWGS84, Is.EqualTo(1.0).Within(1e-12));
            Assert.That(c.VerticalDepthWGS84, Is.EqualTo(150.0).Within(1e-12));
            Assert.That(c.OctreeCode, Is.Not.Null);
            Assert.That(c.OctreeDepth, Is.EqualTo(8));

            // List IDs and MetaInfo
            var ids = (List<Guid>)await client.GetAllGeodeticConversionSetIdAsync();
            Assert.That(ids, Does.Contain(id));
            var metas = (List<MetaInfo>)await client.GetAllGeodeticConversionSetMetaInfoAsync();
            Assert.That(metas.Any(m => m.ID == id));

            // DELETE
            await client.DeleteGeodeticConversionSetByIdAsync(id);
            try
            {
                await client.GetGeodeticConversionSetByIdAsync(id);
                Assert.Fail("Expected 404 after deletion");
            }
            catch (ApiException ex)
            {
                Assert.That(ex.StatusCode, Is.EqualTo(404));
            }
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (httpClient != null)
            {
                httpClient.Dispose();
                httpClient = null!;
            }
        }
    }
}

