using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Legislator> GetLegislator( string id )
        {
            try
            {
                string url = String.Format( "legislators/{0}?apikey={1}", id, apiToken );
                var response = await client.GetAsync( url );
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Legislator>();
            }
            catch ( HttpRequestException e )
            {
                throw new OpenStatesHttpException( "Unexpected response from server.", e );
            }
        }

        public async Task<IEnumerable<Legislator>> LegislatorsGeoLookup( double latitude, double longitude )
        {
            try
            {
                string url = String.Format( "legislators/geo/?lat={0}&long={1}&apikey={2}", latitude, longitude, apiToken );
                var response = await client.GetAsync( url );
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<IEnumerable<Legislator>>();
            }
            catch ( HttpRequestException e )
            {
                throw new OpenStatesHttpException( "Unexpected response from server.", e );
            }
        }

        public async Task<IEnumerable<Legislator>> LegislatorSearch( State? state = null, string firstName = null,
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
            try
            {
                var response = await client.GetAsync( url );
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<IEnumerable<Legislator>>();
            }
            catch ( HttpRequestException e )
            {
                throw new OpenStatesHttpException( "Unexpected response from server.", e );
            }
        }

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
