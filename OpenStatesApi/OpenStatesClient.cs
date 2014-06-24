using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace OpenStatesApi
{
    public class OpenStatesClient : IDisposable
    {
        HttpClient client;
        string apiToken;

        public OpenStatesClient( string apiKey )
        {
            client = new HttpClient();
            client.BaseAddress = new Uri( "http://openstates.org/api/v1/" );
            apiToken = apiKey;
        }

        internal OpenStatesClient( string apiKey, HttpMessageHandler messageHandler )
        {
            client = new HttpClient( messageHandler );
            client.BaseAddress = new Uri( "http://openstates.org/api/v1/" );
            apiToken = apiKey;
        }


        #region Metadata Methods

        public async Task<IEnumerable<MetadataOverview>> Metadata()
        {
            var urlParameters = new Dictionary<string, string>();
            urlParameters.Add( "apikey", apiToken );
            string url = "metadata" + urlParameters.ToQueryString();
            var response = await client.GetAsync( url );
            response.Check();
            return await response.Content.ReadAsAsync<IEnumerable<MetadataOverview>>();
        }

        public async Task<Metadata> Metadata( State state )
        {
            var urlParameters = new Dictionary<string, string>();
            urlParameters.Add( "apikey", apiToken );
            string url = String.Format( "metadata/{0}", state.ToString() ) + urlParameters.ToQueryString();
            var response = await client.GetAsync( url );
            response.Check();
            return await response.Content.ReadAsAsync<Metadata>();
        }

        #endregion


        #region Legislator Methods

        public async Task<Legislator> GetLegislator( string id )
        {
            var urlParameters = new Dictionary<string, string>();
            urlParameters.Add( "apikey", apiToken );
            string url = String.Format( "legislators/{0}", id ) + urlParameters.ToQueryString();
            var response = await client.GetAsync( url );
            response.Check();
            return await response.Content.ReadAsAsync<Legislator>();
        }

        public async Task<IEnumerable<Legislator>> LegislatorsGeoLookup( double latitude, double longitude )
        {
            var urlParameters = new Dictionary<string, string>();
            urlParameters.Add( "lat", latitude.ToString() );
            urlParameters.Add( "long", longitude.ToString() );
            urlParameters.Add( "apikey", apiToken );
            string url = "legislators/geo/" + urlParameters.ToQueryString();
            var response = await client.GetAsync( url );
            response.Check();
            return await response.Content.ReadAsAsync<IEnumerable<Legislator>>();
        }

        public async Task<IEnumerable<LegislatorOverview>> LegislatorSearch( State? state = null, string firstName = null,
            string lastName = null, Chamber? chamber = null, bool active = true, string term = null, string district = null,
            string party = null )
        {
            var urlParameters = new Dictionary<string, string>();
            if ( state != null )
            {
                urlParameters.Add( "state", state.ToString() );
            }
            if ( !String.IsNullOrEmpty( firstName ) )
            {
                urlParameters.Add( "first_name", firstName );
            }
            if ( !String.IsNullOrEmpty( lastName ) )
            {
                urlParameters.Add( "last_name", lastName );
            }
            if ( chamber != null )
            {
                urlParameters.Add( "chamber", chamber.ToString() );
            }
            if ( !active )
            {
                urlParameters.Add( "active", "false" );
            }
            if ( !String.IsNullOrEmpty( term ) )
            {
                urlParameters.Add( "term", term );
            }
            if ( !String.IsNullOrEmpty( district ) )
            {
                urlParameters.Add( "district", district );
            }
            urlParameters.Add( "apikey", apiToken );
            string url = "legislators/" + urlParameters.ToQueryString();
            var response = await client.GetAsync( url );
            response.Check();
            return await response.Content.ReadAsAsync<IEnumerable<LegislatorOverview>>();
        }

        #endregion

        #region District Methods

        public async Task<IEnumerable<District>> DistrictSearch(State state, Chamber? chamber = null)
        {
            var urlParameters = new Dictionary<string, string>();
            urlParameters.Add( "apikey", apiToken );
            string url = String.Format( "districts/{0}", state.ToString() );
            if ( chamber.HasValue )
            {
                url += String.Format( "/{0}", chamber.ToString() );
            }
            url += urlParameters.ToQueryString();
            var response = await client.GetAsync( url );
            response.Check();
            return await response.Content.ReadAsAsync<IEnumerable<District>>();
        }

        public async Task<DistrictBoundary> DistrictBoundaryLookup( string boundaryId )
        {
            var urlParameters = new Dictionary<string, string>();
            urlParameters.Add( "apikey", apiToken );
            string url = String.Format( "districts/boundary/{0}", boundaryId );
            url += urlParameters.ToQueryString();
            var response = await client.GetAsync( url );
            response.Check();
            return await response.Content.ReadAsAsync<DistrictBoundary>();
        }

        #endregion


        #region IDisposable Implementation

        private bool disposed;
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( !this.disposed )
            {
                if ( disposing )
                {
                    if ( client != null )
                    {
                        client.Dispose();
                        client = null;
                    }
                }
            }
            this.disposed = true;
        }

        #endregion
    }
}
