using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
