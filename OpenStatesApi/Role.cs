using Newtonsoft.Json;
using OpenStatesApi.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenStatesApi
{
    public class Role
    {
        public string Term { get; set; }
        public Chamber Chamber { get; set; }
        public State State { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [JsonConverter( typeof( RoleTypeConverter ) )]
        public RoleType Type { get; set; }
        public string Party { get; set; }
        public string District { get; set; }
        public string Committee { get; set; }
        public string Subcommittee { get; set; }
        [JsonProperty( "committee_id" )]
        public string CommitteeId { get; set; }
        public string Position { get; set; }
    }
}
