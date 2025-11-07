using System.Net.Http.Headers;
using NORCE.Drilling.GeodeticDatum.ModelShared;

namespace ServiceTest
{
    public class GeodeticDatumApiTests
    {
        // testing outside Visual Studio requires using http port (https faces authentication issues both in console and on github)
        //private static string host = "https://localhost:5001/";
        //private static string host = "http://localhost:5002/";
        private static string host = "http://localhost:8080/";
        //private static string host = "https://dev.DigiWells.no/";
        //private static string host = "https://app.DigiWells.no/";
        private static HttpClient httpClient;
        private static Client nSwagClient;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }; // temporary workaround for testing purposes: bypass certificate validation (not recommended for production environments due to security risks)
            httpClient = new HttpClient(handler);
            httpClient.BaseAddress = new Uri(host + "GeodeticDatum/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            nSwagClient = new Client(httpClient.BaseAddress.ToString(), httpClient);
        }

        [Test]
        public async Task Test_GeodeticDatum_GET()
        {
            #region post a GeodeticDatum
            Guid guid = Guid.NewGuid();
            MetaInfo metaInfo = new() { ID = guid };
            DateTimeOffset creationDate = DateTimeOffset.UtcNow;
            GeodeticDatum geodeticDatum = new()
            {
                MetaInfo = metaInfo,
                Name = "My test GeodeticDatum",
            };

            try
            {
                await nSwagClient.PostGeodeticDatumAsync(geodeticDatum);
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to POST given GeodeticDatum\n" + ex.Message);
            }
            #endregion

            #region GetAllGeodeticDatumId
            List<Guid> idList = [];
            try
            {
                idList = (List<Guid>)await nSwagClient.GetAllGeodeticDatumIdAsync();
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to GET all GeodeticDatum ids\n" + ex.Message);
            }
            Assert.That(idList, Is.Not.Null);
            Assert.That(idList, Does.Contain(guid));
            #endregion

            #region GetAllGeodeticDatumMetaInfo
            List<MetaInfo> metaInfoList = [];
            try
            {
                metaInfoList = (List<MetaInfo>)await nSwagClient.GetAllGeodeticDatumMetaInfoAsync();
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to GET all GeodeticDatum metainfos\n" + ex.Message);
            }
            Assert.That(metaInfoList, Is.Not.Null);
            IEnumerable<MetaInfo> metaInfoList2 =
                from elt in metaInfoList
                where elt.ID == guid
                select elt;
            Assert.That(metaInfoList2, Is.Not.Null);
            Assert.That(metaInfoList2, Is.Not.Empty);
            #endregion

            #region GetAllGeodeticDatumById
            GeodeticDatum? geodeticDatum2 = null;
            try
            {
                geodeticDatum2 = await nSwagClient.GetGeodeticDatumByIdAsync(guid);
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to GET GeodeticDatum of given Id\n" + ex.Message);
            }
            Assert.That(geodeticDatum2, Is.Not.Null);
            Assert.That(geodeticDatum2.Name, Is.EqualTo(geodeticDatum.Name));
            #endregion

            #region GetAllGeodeticDatumLight
            List<GeodeticDatumLight> geodeticDatumLightList = [];
            try
            {
                geodeticDatumLightList = (List<GeodeticDatumLight>)await nSwagClient.GetAllGeodeticDatumLightAsync();
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to GET the list of GeodeticDatumLight\n" + ex.Message);
            }
            Assert.That(geodeticDatumLightList, Is.Not.Null);
            Assert.That(geodeticDatumLightList, Is.Not.Empty);
            IEnumerable<GeodeticDatumLight> geodeticDatumLightList2 =
                from elt in geodeticDatumLightList
                where elt.Name == geodeticDatum.Name
                select elt;
            Assert.That(geodeticDatumLightList2, Is.Not.Null);
            Assert.That(geodeticDatumLightList2, Is.Not.Empty);
            #endregion

            #region GetAllGeodeticDatum
            List<GeodeticDatum> geodeticDatumList = new();
            try
            {
                geodeticDatumList = (List<GeodeticDatum>)await nSwagClient.GetAllGeodeticDatumAsync();
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to GET the list of GeodeticDatum\n" + ex.Message);
            }
            Assert.That(geodeticDatumList, Is.Not.Null);
            IEnumerable<GeodeticDatum> geodeticDatumList2 =
                from elt in geodeticDatumList
                where elt.Name == geodeticDatum.Name
                select elt;
            Assert.That(geodeticDatumList2, Is.Not.Null);
            Assert.That(geodeticDatumList2, Is.Not.Empty);
            #endregion

            #region finally delete the new ID
            geodeticDatum2 = null;
            try
            {
                await nSwagClient.DeleteGeodeticDatumByIdAsync(guid);
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to DELETE GeodeticDatum of given Id\n" + ex.Message);
            }
            try
            {
                geodeticDatum2 = await nSwagClient.GetGeodeticDatumByIdAsync(guid);
            }
            catch (ApiException ex)
            {
                Assert.That(ex.StatusCode, Is.EqualTo(404));
                TestContext.WriteLine("Impossible to GET GeodeticDatum of given Id\n" + ex.Message);
            }
            Assert.That(geodeticDatum2, Is.Null);
            #endregion
        }

        [Test]
        public async Task Test_GeodeticDatum_POST()
        {
            #region trying to post an empty guid
            Guid guid = Guid.Empty;
            MetaInfo metaInfo = new() { ID = guid };
            DateTimeOffset creationDate = DateTimeOffset.UtcNow;
            GeodeticDatum geodeticDatum = new()
            {
                MetaInfo = metaInfo,
                Name = "My test GeodeticDatum",
                Description = "My test GeodeticDatum",
                CreationDate = creationDate,
                LastModificationDate = creationDate,
            };
            GeodeticDatum? geodeticDatum2 = null;
            try
            {
                await nSwagClient.PostGeodeticDatumAsync(geodeticDatum);
            }
            catch (ApiException ex)
            {
                Assert.That(ex.StatusCode, Is.EqualTo(400));
                TestContext.WriteLine("Impossible to POST GeodeticDatum with empty Guid\n" + ex.Message);
            }
            try
            {
                geodeticDatum2 = await nSwagClient.GetGeodeticDatumByIdAsync(Guid.Empty);
            }
            catch (ApiException ex)
            {
                Assert.That(ex.StatusCode, Is.EqualTo(400));
                TestContext.WriteLine("Impossible to GET GeodeticDatum identified by an empty Guid\n" + ex.Message);
            }
            Assert.That(geodeticDatum2, Is.Null);
            #endregion

            #region post some corrupted data
            // post data with missing input that fails the calculation process
            #endregion

            #region posting a new ID in a valid state
            guid = Guid.NewGuid();
            metaInfo = new() { ID = guid };
            geodeticDatum.MetaInfo = metaInfo;
            try
            {
                await nSwagClient.PostGeodeticDatumAsync(geodeticDatum);
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to POST GeodeticDatum although it is in a valid state\n" + ex.Message);
            }
            try
            {
                geodeticDatum2 = await nSwagClient.GetGeodeticDatumByIdAsync(guid);
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to GET the GeodeticDatum of given Id\n" + ex.Message);
            }
            Assert.That(geodeticDatum2, Is.Not.Null);
            Assert.That(geodeticDatum2.MetaInfo, Is.Not.Null);
            Assert.That(geodeticDatum2.MetaInfo.ID, Is.EqualTo(guid));
            Assert.That(geodeticDatum2.Name, Is.EqualTo(geodeticDatum.Name));
            #endregion

            #region trying to repost the same ID
            bool conflict = false;
            try
            {
                await nSwagClient.PostGeodeticDatumAsync(geodeticDatum);
            }
            catch (ApiException ex)
            {
                conflict = true;
                Assert.That(ex.StatusCode, Is.EqualTo(409));
                TestContext.WriteLine("Impossible to POST existing GeodeticDatum\n" + ex.Message);
            }
            Assert.That(conflict, Is.True);
            #endregion

            #region finally delete the new ID
            geodeticDatum2 = null;
            try
            {
                await nSwagClient.DeleteGeodeticDatumByIdAsync(guid);
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to DELETE GeodeticDatum of given Id\n" + ex.Message);
            }
            try
            {
                geodeticDatum2 = await nSwagClient.GetGeodeticDatumByIdAsync(guid);
            }
            catch (ApiException ex)
            {
                Assert.That(ex.StatusCode, Is.EqualTo(404));
                TestContext.WriteLine("Impossible to GET deleted GeodeticDatum of given Id\n" + ex.Message);
            }
            Assert.That(geodeticDatum2, Is.Null);
            #endregion
        }

        [Test]
        public async Task Test_GeodeticDatum_PUT()
        {
            #region posting a new ID
            Guid guid = Guid.NewGuid();
            MetaInfo metaInfo = new() { ID = guid };
            DateTimeOffset creationDate = DateTimeOffset.UtcNow;
            GeodeticDatum geodeticDatum = new()
            {
                MetaInfo = metaInfo,
                Name = "My test GeodeticDatum",
                Description = "My test GeodeticDatum",
                CreationDate = creationDate,
                LastModificationDate = creationDate,
            };
            GeodeticDatum? geodeticDatum2 = null;
            try
            {
                await nSwagClient.PostGeodeticDatumAsync(geodeticDatum);
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to POST GeodeticDatum\n" + ex.Message);
            }
            try
            {
                geodeticDatum2 = await nSwagClient.GetGeodeticDatumByIdAsync(guid);
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to GET the GeodeticDatum of given Id\n" + ex.Message);
            }
            Assert.That(geodeticDatum2, Is.Not.Null);
            Assert.That(geodeticDatum2.MetaInfo, Is.Not.Null);
            Assert.That(geodeticDatum2.MetaInfo.ID, Is.EqualTo(guid));
            Assert.That(geodeticDatum2.Name, Is.EqualTo(geodeticDatum.Name));
            #endregion

            #region updating the new Id
            geodeticDatum.Name = "My test GeodeticDatum with modified name";
            geodeticDatum.LastModificationDate = DateTimeOffset.UtcNow;
            try
            {
                await nSwagClient.PutGeodeticDatumByIdAsync(geodeticDatum.MetaInfo.ID, geodeticDatum);
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to PUT GeodeticDatum of given Id\n" + ex.Message);
            }
            try
            {
                geodeticDatum2 = await nSwagClient.GetGeodeticDatumByIdAsync(guid);
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to GET the updated GeodeticDatum of given Id\n" + ex.Message);
            }
            Assert.That(geodeticDatum2, Is.Not.Null);
            Assert.That(geodeticDatum2.MetaInfo, Is.Not.Null);
            Assert.That(geodeticDatum2.MetaInfo.ID, Is.EqualTo(geodeticDatum.MetaInfo.ID));
            Assert.That(geodeticDatum2.Name, Is.EqualTo(geodeticDatum.Name));
            #endregion

            #region finally delete the new ID
            geodeticDatum2 = null;
            try
            {
                await nSwagClient.DeleteGeodeticDatumByIdAsync(guid);
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to DELETE GeodeticDatum of given Id\n" + ex.Message);
            }
            try
            {
                geodeticDatum2 = await nSwagClient.GetGeodeticDatumByIdAsync(guid);
            }
            catch (ApiException ex)
            {
                Assert.That(ex.StatusCode, Is.EqualTo(404));
                TestContext.WriteLine("Impossible to GET deleted GeodeticDatum of given Id\n" + ex.Message);
            }
            Assert.That(geodeticDatum2, Is.Null);
            #endregion
        }

        [Test]
        public async Task Test_GeodeticDatum_DELETE()
        {
            #region posting a new ID
            Guid guid = Guid.NewGuid();
            MetaInfo metaInfo = new() { ID = guid };
            DateTimeOffset creationDate = DateTimeOffset.UtcNow;
            GeodeticDatum geodeticDatum = new()
            {
                MetaInfo = metaInfo,
                Name = "My test GeodeticDatum",
            };
            GeodeticDatum? geodeticDatum2 = null;
            try
            {
                await nSwagClient.PostGeodeticDatumAsync(geodeticDatum);
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to POST GeodeticDatum\n" + ex.Message);
            }
            try
            {
                geodeticDatum2 = await nSwagClient.GetGeodeticDatumByIdAsync(guid);
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to GET the GeodeticDatum of given Id\n" + ex.Message);
            }
            Assert.That(geodeticDatum2, Is.Not.Null);
            Assert.That(geodeticDatum2.MetaInfo, Is.Not.Null);
            Assert.That(geodeticDatum2.MetaInfo.ID, Is.EqualTo(geodeticDatum.MetaInfo.ID));
            Assert.That(geodeticDatum2.Name, Is.EqualTo(geodeticDatum.Name));
            #endregion

            #region finally delete the new ID
            geodeticDatum2 = null;
            try
            {
                await nSwagClient.DeleteGeodeticDatumByIdAsync(guid);
            }
            catch (ApiException ex)
            {
                TestContext.WriteLine("Impossible to DELETE GeodeticDatum of given Id\n" + ex.Message);
            }
            try
            {
                geodeticDatum2 = await nSwagClient.GetGeodeticDatumByIdAsync(guid);
            }
            catch (ApiException ex)
            {
                Assert.That(ex.StatusCode, Is.EqualTo(404));
                TestContext.WriteLine("Impossible to GET deleted GeodeticDatum of given Id\n" + ex.Message);
            }
            Assert.That(geodeticDatum2, Is.Null);
            #endregion
        }

        //#endif

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            httpClient?.Dispose();
        }
    }
}