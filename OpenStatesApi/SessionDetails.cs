using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenStatesApi
{
    public class SessionDetails
    {
        public SessionType Type { get; set; }
        [JsonProperty( "display_name" )]
        public string DisplayName { get; set; }
        [JsonProperty( "start_date" )]
        public DateTime StartDate { get; set; }
        [JsonProperty( "end_date" )]
        public DateTime EndDate { get; set; }
    }
}
