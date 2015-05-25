using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenStatesApi
{
    public class CommitteeMember
    {
        [JsonProperty( "leg_id" )]
        public string LegislatorId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
