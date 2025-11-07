using System.Net.Http.Headers;
using NORCE.Drilling.GeodeticDatum.ModelShared;

namespace ServiceTest
{
    public class SpheroidApiTests
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
        public async Task Spheroid_CRUD_EndToEnd()
        {
            // Create
            var id = Guid.NewGuid();
            var spheroid = new Spheroid
            {
                MetaInfo = new MetaInfo { ID = id },
                Name = "Test Spheroid",
                Description = "Created by API test",
                SemiMajorAxis = new ScalarDrillingProperty { DiracDistributionValue = new DiracDistribution { Value = 6378137 } },
                SemiMinorAxis = new ScalarDrillingProperty { DiracDistributionValue = new DiracDistribution { Value = 6356752.3142 } },
                IsSemiMajorAxisSet = true,
                IsSemiMinorAxisSet = true
            };

            try
            {
                await client.PostSpheroidAsync(spheroid);
            }
            catch (ApiException ex)
            {
                Assert.Fail($"POST Spheroid failed: {ex.Message}");
            }

            // List IDs
            List<Guid> ids = new();
            try
            {
                ids = (List<Guid>)await client.GetAllSpheroidIdAsync();
            }
            catch (ApiException ex)
            {
                Assert.Fail($"GET Spheroid IDs failed: {ex.Message}");
            }
            Assert.That(ids, Does.Contain(id));

            // Get MetaInfo list
            List<MetaInfo> metas = new();
            try
            {
                metas = (List<MetaInfo>)await client.GetAllSpheroidMetaInfoAsync();
            }
            catch (ApiException ex)
            {
                Assert.Fail($"GET Spheroid MetaInfo failed: {ex.Message}");
            }
            Assert.That(metas.Any(m => m.ID == id), Is.True);

            // Get by ID
            Spheroid? fetched = null;
            try
            {
                fetched = await client.GetSpheroidByIdAsync(id);
            }
            catch (ApiException ex)
            {
                Assert.Fail($"GET Spheroid by ID failed: {ex.Message}");
            }
            Assert.That(fetched, Is.Not.Null);
            Assert.That(fetched!.Name, Is.EqualTo("Test Spheroid"));

            // Update
            fetched.Description = "Updated by API test";
            try
            {
                await client.PutSpheroidByIdAsync(id, fetched);
            }
            catch (ApiException ex)
            {
                Assert.Fail($"PUT Spheroid failed: {ex.Message}");
            }
            var updated = await client.GetSpheroidByIdAsync(id);
            Assert.That(updated.Description, Is.EqualTo("Updated by API test"));

            // List all heavy
            List<Spheroid> all = new();
            try
            {
                all = (List<Spheroid>)await client.GetAllSpheroidAsync();
            }
            catch (ApiException ex)
            {
                Assert.Fail($"GET all Spheroid failed: {ex.Message}");
            }
            Assert.That(all.Any(s => s.MetaInfo?.ID == id), Is.True);

            // Delete
            try
            {
                await client.DeleteSpheroidByIdAsync(id);
            }
            catch (ApiException ex)
            {
                Assert.Fail($"DELETE Spheroid failed: {ex.Message}");
            }

            // Ensure deleted
            try
            {
                await client.GetSpheroidByIdAsync(id);
                Assert.Fail("Expected NotFound for deleted Spheroid");
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

