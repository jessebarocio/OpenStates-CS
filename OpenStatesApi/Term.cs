using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenStatesApi
{
    public class Term
    {
        [JsonProperty( "start_year" )]
        public int StartYear { get; set; }
        [JsonProperty( "end_yar" )]
        public int EndYear { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Sessions { get; set; }
    }
}
