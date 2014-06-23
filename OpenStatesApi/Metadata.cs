using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStatesApi
{
    public class Metadata
    {
        public string Abbreviation { get; set; }
        [JsonProperty("capitol_timezone")]
        public string CapitolTimezone { get; set; }
        public IDictionary<Chamber, ChamberDetails> Chambers { get; set; }
        [JsonProperty( "feature_flags" )]
        public IEnumerable<string> FeatureFlags { get; set; }
        [JsonProperty( "latest_csv_date" )]
        public DateTime LatestCsvDate { get; set; }
        [JsonProperty( "latest_csv_url" )]
        public string LatestCsvUrl { get; set; }
        [JsonProperty( "latest_json_date" )]
        public DateTime LatestJsonDate { get; set; }
        [JsonProperty( "latest_json_url" )]
        public string LatestJsonUrl { get; set; }
        [JsonProperty( "latest_update" )]
        public DateTime LatestUpdate { get; set; }
        [JsonProperty( "legislature_name" )]
        public string LegislatureName { get; set; }
        [JsonProperty( "legislature_url" )]
        public string LegislatureUrl { get; set; }
        public string Name { get; set; }
        [JsonProperty( "session_details" )]
        public IDictionary<string, SessionDetails> SessionDetails { get; set; }
        public IEnumerable<Term> Terms { get; set; }
    }
}
