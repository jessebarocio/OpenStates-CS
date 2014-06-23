using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStatesApi
{
    public class MetadataOverview
    {
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public IDictionary<Chamber, ChamberDetails> Chambers { get; set; }
        [JsonProperty( "feature_flags" )]
        public IEnumerable<string> FeatureFlags { get; set; }
    }
}
