﻿using NUnit.Framework;
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

namespace OpenStatesApi.Tests.Unit
{
    [TestFixture( Category = "Unit Tests" )]
    public class OpenStatesClientTests
    {
        OpenStatesClient client;
        FakeHttpMessageHandler fakeHttpHandler;
        HttpResponseMessage fakeResponse;

        [SetUp]
        public void SetUp()
        {
            // Setup a fake response
            fakeResponse = new HttpResponseMessage( HttpStatusCode.OK );
            // Pass in a fake handler, which will return the response. 
            // The fake handler also allows us to inspect the HttpRequestMessage.
            fakeHttpHandler = new FakeHttpMessageHandler( fakeResponse );
            client = new OpenStatesClient( "MyApiKey", fakeHttpHandler );
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


        #region Test Metadata Methods

        [Test]
        public async void Metadata_MakesCorrectHttpCall()
        {
            // Arrange
            fakeResponse.Content = GetContentFromEmbeddedJsonResource( "OpenStatesApi.Tests.TestData.MetadataOverview.json" );
            // Act
            var result = await client.Metadata();
            // Assert
            var request = fakeHttpHandler.Request;
            Assert.AreEqual( HttpMethod.Get, request.Method );
            Assert.AreEqual( "http://openstates.org/api/v1/metadata?apikey=MyApiKey", request.RequestUri.ToString() );
        }

        [Test]
        public async void Metadata_MakesCorrectHttpCallWithState()
        {
            // Arrange
            fakeResponse.Content = GetContentFromEmbeddedJsonResource( "OpenStatesApi.Tests.TestData.MetadataFull.json" );
            // Act
            var result = await client.Metadata( State.UT );
            // Assert
            var request = fakeHttpHandler.Request;
            Assert.AreEqual( HttpMethod.Get, request.Method );
            Assert.AreEqual( "http://openstates.org/api/v1/metadata/UT?apikey=MyApiKey", request.RequestUri.ToString() );
        } 

        #endregion


        #region Test Legislator Methods

        [Test]
        public async void GetLegislator_MakesCorrectHttpCall()
        {
            // Arrange
            fakeResponse.Content = GetContentFromEmbeddedJsonResource( "OpenStatesApi.Tests.TestData.Legislator.json" );
            // Act
            var result = await client.GetLegislator( "UTL000064" );
            // Assert
            var request = fakeHttpHandler.Request;
            Assert.AreEqual( HttpMethod.Get, request.Method );
            Assert.AreEqual( "http://openstates.org/api/v1/legislators/UTL000064?apikey=MyApiKey", request.RequestUri.ToString() );
        }

        [Test]
        [ExpectedException( typeof( OpenStatesHttpException ) )]
        public async void GetLegislator_ThrowsOpenStatesHttpException()
        {
            // Arrange
            fakeResponse.Content = GetContentFromEmbeddedJsonResource( "OpenStatesApi.Tests.TestData.Legislator.json" );
            fakeResponse.StatusCode = HttpStatusCode.BadRequest;
            // Act
            var result = await client.GetLegislator( "UTL000064" );
            // Assert - should throw an OpenStatesHttpException
        }


        [Test]
        public async void LegislatorGeoLookup_MakesCorrectHttpCall()
        {
            // Arrange
            fakeResponse.Content = GetContentFromEmbeddedJsonResource( "OpenStatesApi.Tests.TestData.LegislatorArray.json" );
            // Act
            var result = await client.LegislatorsGeoLookup( 41.082303, -111.996914 );
            // Assert
            var request = fakeHttpHandler.Request;
            Assert.AreEqual( HttpMethod.Get, request.Method );
            Assert.AreEqual( "http://openstates.org/api/v1/legislators/geo/?lat=41.082303&long=-111.996914&apikey=MyApiKey", request.RequestUri.ToString() );
        }

        [Test]
        [ExpectedException( typeof( OpenStatesHttpException ) )]
        public async void LegislatorGeoLookup_ThrowsOpenStatesHttpException()
        {
            // Arrange
            fakeResponse.Content = GetContentFromEmbeddedJsonResource( "OpenStatesApi.Tests.TestData.LegislatorArray.json" );
            fakeResponse.StatusCode = HttpStatusCode.BadRequest;
            // Act
            var result = await client.LegislatorsGeoLookup( 41.082303, -111.996914 );
            // Assert - should throw an OpenStatesHttpException
        }

        [Test]
        public async void LegislatorSearch_MakesCorrectHttpCall()
        {
            // Arrange
            fakeResponse.Content = GetContentFromEmbeddedJsonResource( "OpenStatesApi.Tests.TestData.LegislatorArray.json" );
            // Act
            var result = await client.LegislatorSearch();
            // Assert
            var request = fakeHttpHandler.Request;
            Assert.AreEqual( HttpMethod.Get, request.Method );
            Assert.AreEqual( "http://openstates.org/api/v1/legislators/?apikey=MyApiKey", request.RequestUri.ToString() );
        }

        [Test]
        public async void LegislatorSearch_MakesCorrectHttpCallWithParameters()
        {
            // Arrange
            fakeResponse.Content = GetContentFromEmbeddedJsonResource( "OpenStatesApi.Tests.TestData.LegislatorArray.json" );
            // Act
            var result = await client.LegislatorSearch(
                state: State.UT,
                firstName: "Jerry",
                chamber: Chamber.Upper );
            // Assert
            var request = fakeHttpHandler.Request;
            Assert.AreEqual( HttpMethod.Get, request.Method );
            Assert.AreEqual( "http://openstates.org/api/v1/legislators/?state=UT&first_name=Jerry&chamber=Upper&apikey=MyApiKey", request.RequestUri.ToString() );
        }

        [Test]
        [ExpectedException( typeof( OpenStatesHttpException ) )]
        public async void LegislatorSearch_ThrowsOpenStatesHttpException()
        {
            // Arrange
            fakeResponse.Content = GetContentFromEmbeddedJsonResource( "OpenStatesApi.Tests.TestData.LegislatorArray.json" );
            fakeResponse.StatusCode = HttpStatusCode.BadRequest;
            // Act
            var result = await client.LegislatorSearch(
                state: State.UT,
                firstName: "Jerry",
                chamber: Chamber.Upper );
            // Assert - should throw an OpenStatesHttpException
        } 

        #endregion


        #region Test Committee Methods

        [Test]
        public async void GetCommittee_MakesCorrectHttpCall()
        {
            // Arrange
            fakeResponse.Content = GetContentFromEmbeddedJsonResource("OpenStatesApi.Tests.TestData.Committee.json");
            // Act
            var result = await client.GetCommittee("PAC000007");
            // Assert
            var request = fakeHttpHandler.Request;
            Assert.AreEqual(HttpMethod.Get, request.Method);
            Assert.AreEqual("http://openstates.org/api/v1/committees/PAC000007?apikey=MyApiKey",
                request.RequestUri.ToString());
        }

        [Test]
        [ExpectedException(typeof (OpenStatesHttpException))]
        public async void GetCommittee_ThrowsOpenStatesHttpException()
        {
            // Arrange
            fakeResponse.Content = GetContentFromEmbeddedJsonResource("OpenStatesApi.Tests.TestData.Committee.json");
            fakeResponse.StatusCode = HttpStatusCode.BadRequest;
            // Act
            var result = await client.GetCommittee("PAC000007");
            // Assert - should throw an OpenStatesHttpException
        }

        [Test]
        public async void CommitteeSearch_MakesCorrectHttpCall()
        {
            // Arrange
            fakeResponse.Content = GetContentFromEmbeddedJsonResource( "OpenStatesApi.Tests.TestData.CommitteeArray.json" );
            // Act
            var result = await client.CommitteeSearch();
            // Assert
            var request = fakeHttpHandler.Request;
            Assert.AreEqual(HttpMethod.Get, request.Method);
            Assert.AreEqual("http://openstates.org/api/v1/committees/?apikey=MyApiKey", request.RequestUri.ToString());
        }

        [Test]
        public async void CommitteeSearch_MakesCorrectHttpCallWithParameters()
        {
            // Arrange
            fakeResponse.Content = GetContentFromEmbeddedJsonResource( "OpenStatesApi.Tests.TestData.CommitteeArray.json" );
            // Act
            var result = await client.CommitteeSearch(
                state: State.PA, 
                chamber: Chamber.Upper);
            // Assert
            var request = fakeHttpHandler.Request;
            Assert.AreEqual(HttpMethod.Get, request.Method);
            Assert.AreEqual("http://openstates.org/api/v1/committees/?state=PA&chamber=Upper&apikey=MyApiKey",
                request.RequestUri.ToString());
        }

        [Test]
        [ExpectedException(typeof (OpenStatesHttpException))]
        public async void CommitteeSearch_ThrowsOpenStatesHttpException()
        {
            // Arrange
            fakeResponse.Content =
                GetContentFromEmbeddedJsonResource("OpenStatesApi.Tests.TestData.LegislatorArray.json");
            fakeResponse.StatusCode = HttpStatusCode.BadRequest;
            // Act
            var result = await client.CommitteeSearch(
                state: State.PA,
                chamber: Chamber.Upper);
            // Assert - should throw an OpenStatesHttpException
        }

        #endregion


        #region Test District Methods

        [Test]
        public async void DistrictSearch_MakesCorrectHttpCall()
        {
            // Arrange
            fakeResponse.Content = GetContentFromEmbeddedJsonResource( "OpenStatesApi.Tests.TestData.DistrictSearch.json" );
            // Act
            var result = await client.DistrictSearch(State.UT);
            // Assert
            var request = fakeHttpHandler.Request;
            Assert.AreEqual( HttpMethod.Get, request.Method );
            Assert.AreEqual( "http://openstates.org/api/v1/districts/UT?apikey=MyApiKey", request.RequestUri.ToString() );
        }

        #endregion

        [Test]
        public async void DistrictBoundaryLookup_MakesCorrectHttpCall()
        {
            // Arrange
            fakeResponse.Content = GetContentFromEmbeddedJsonResource( "OpenStatesApi.Tests.TestData.DistrictBoundary.json" );
            // Act
            var result = await client.DistrictBoundaryLookup( "sldl/ut-22" );
            // Assert
            var request = fakeHttpHandler.Request;
            Assert.AreEqual( HttpMethod.Get, request.Method );
            Assert.AreEqual( "http://openstates.org/api/v1/districts/boundary/sldl/ut-22?apikey=MyApiKey", request.RequestUri.ToString() );
        }


        private static HttpContent GetContentFromEmbeddedJsonResource( string resourcePath )
        {
            using ( var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream( resourcePath ) )
            using ( var reader = new StreamReader( stream ) )
            {
                return new StringContent( reader.ReadToEnd(), Encoding.UTF8, "application/json" );
            }
        }
    }
}
