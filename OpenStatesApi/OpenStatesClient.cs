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
        string apiKey;

        public OpenStatesClient( string apiKey )
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri( "http://openstates.org/api/v1/" );
            this.apiKey = apiKey;
        }

        internal OpenStatesClient( string apiKey, HttpMessageHandler messageHandler )
        {
            this.client = new HttpClient( messageHandler );
            this.client.BaseAddress = new Uri( "http://openstates.org/api/v1/" );
            this.apiKey = apiKey;
        }


        #region Metadata Methods

        public async Task<IEnumerable<MetadataOverview>> Metadata()
        {
            return await MakeRequest<IEnumerable<MetadataOverview>>( "metadata" );
        }

        public async Task<Metadata> Metadata( State state )
        {
            return await MakeRequest<Metadata>( String.Format( "metadata/{0}", state.ToString() ) );
        }

        #endregion


        #region Legislator Methods

        public async Task<Legislator> GetLegislator( string id )
        {
            return await MakeRequest<Legislator>( String.Format( "legislators/{0}", id ) );
        }

        public async Task<IEnumerable<Legislator>> LegislatorsGeoLookup( double latitude, double longitude )
        {
            var urlParameters = new Dictionary<string, string>();
            urlParameters.Add( "lat", latitude.ToString() );
            urlParameters.Add( "long", longitude.ToString() );
            string url = "legislators/geo/";
            return await MakeRequest<IEnumerable<Legislator>>( url, urlParameters );
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
            string url = "legislators/";
            return await MakeRequest<IEnumerable<LegislatorOverview>>( url, urlParameters );
        }

        #endregion


        #region Committee Methods

        public async Task<Committee> GetCommittee(string id)
        {
            return await MakeRequest<Committee>(String.Format("committees/{0}", id));
        }

        public async Task<IEnumerable<CommitteeOverview>> CommitteeSearch(State? state = null, Chamber? chamber = null, string committeeName = null)
        {
            var urlParameters = new Dictionary<string, string>();
            if (state != null)
            {
                urlParameters.Add("state", state.ToString());
            }
            if (chamber != null)
            {
                urlParameters.Add("chamber", chamber.ToString());
            }
            if (!String.IsNullOrEmpty(committeeName))
            {
                urlParameters.Add("committee", committeeName);
            }
            string url = "committees/";
            return await MakeRequest<IEnumerable<CommitteeOverview>>( url, urlParameters );
        }

        #endregion


        #region District Methods

        public async Task<IEnumerable<District>> DistrictSearch( State state, Chamber? chamber = null )
        {
            string url = String.Format( "districts/{0}", state.ToString() );
            if ( chamber.HasValue )
            {
                url += String.Format( "/{0}", chamber.ToString() );
            }
            return await MakeRequest<IEnumerable<District>>( url );
        }

        public async Task<DistrictBoundary> DistrictBoundaryLookup( string boundaryId )
        {
            return await MakeRequest<DistrictBoundary>( String.Format( "districts/boundary/{0}", boundaryId ) );
        }

        #endregion


        private async Task<T> MakeRequest<T>( string baseUrl, IDictionary<string, string> parameters = null )
        {
            try
            {
                if ( parameters == null )
                {
                    parameters = new Dictionary<string, string>();
                }
                parameters.Add( "apikey", apiKey );
                string url = baseUrl + parameters.ToQueryString();
                var response = await client.GetAsync( url );
                response.Check();
                return await response.Content.ReadAsAsync<T>();
            }
            catch ( OpenStatesException )
            {
                throw;
            }
            catch ( Exception e )
            {
                throw new OpenStatesException( "Operation failed.", e );
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
            if ( !this.disposed && disposing )
            {
                if ( client != null )
                {
                    client.Dispose();
                    client = null;
                }
            }
            this.disposed = true;
        }

        #endregion
    }
}
