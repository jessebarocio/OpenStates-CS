using NUnit.Framework;
using OpenStatesApi.Tests.Fakes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenStatesApi.Tests
{
    [TestFixture(Category = "Unit Tests")]
    public class OpenStatesClientUnitTests
    {
        OpenStatesClient client;
        FakeHttpMessageHandler fakeHttpHandler;
        HttpResponseMessage fakeResponse;

        [SetUp]
        public void SetUp()
        {
            // Setup a fake response
            fakeResponse = new HttpResponseMessage(HttpStatusCode.OK);
            fakeResponse.Content = new StringContent(GetLegislatorJson(), Encoding.UTF8, "application/json");
            // Pass in a fake handler, which will return the response. 
            // The fake handler also allows us to inspect the HttpRequestMessage.
            fakeHttpHandler = new FakeHttpMessageHandler(fakeResponse);
            client = new OpenStatesClient("MyApiKey", fakeHttpHandler);
        }

        [TearDown]
        public void TearDown()
        {
            fakeResponse.Dispose();
            fakeResponse = null;
            fakeHttpHandler.Dispose();
            fakeHttpHandler = null;
            client.Dispose();
            client = null;
        }

        [Test]
        public async void GetLegislator_ReturnsLegislator()
        {
            var result = await client.GetLegislator("UTC000014");
            Assert.IsInstanceOf<Legislator>(result);
        }

        [Test]
        public async void GetLegislator_MakesCorrectHttpCall()
        {
            var result = await client.GetLegislator("UTC000014");
            // Inspect the request
            var request = fakeHttpHandler.Request;
            Assert.AreEqual(HttpMethod.Get, request.Method);
            Assert.AreEqual("http://openstates.org/api/v1/legislators/UTC000014?apikey=MyApiKey", request.RequestUri.ToString());
        }

        [Test]
        [ExpectedException(typeof(OpenStatesHttpException))]
        public async void GetLegislator_ThrowsCustomException()
        {
            // Make the response return an error status code.
            fakeResponse.StatusCode = HttpStatusCode.BadRequest;
            var result = await client.GetLegislator("UTC000014");
        }

        private static string GetLegislatorJson()
        {
            string jsonFile = "OpenStatesApi.Tests.TestData.Legislator1.json";
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(jsonFile))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
