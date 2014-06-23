using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenStatesApi
{
    internal static class ExtensionMethods
    {
        internal static string ToQueryString(this IDictionary<string, string> parameters)
        {
            var array = from key in parameters.Keys
                        select String.Format( "{0}={1}", key, parameters[key] );
            return "?" + String.Join( "&", array );
        }

        internal static void Check( this HttpResponseMessage response )
        {
            if ( !response.IsSuccessStatusCode )
            {
                string exceptionMessage = "";
                switch ( response.StatusCode )
                {
                    case HttpStatusCode.Unauthorized:
                        exceptionMessage = "Invalid API key was specified. Please use a valid Sunlight API key.";
                        break;
                    case HttpStatusCode.NotFound:
                        exceptionMessage = "The requested resource was not found.";
                        break;
                    default:
                        exceptionMessage = "Unexpected response from server. See the StatusCode property for more information.";
                        break;
                }
                throw new OpenStatesHttpException( exceptionMessage )
                {
                    StatusCode = response.StatusCode
                };
            }
        }
    }
}
