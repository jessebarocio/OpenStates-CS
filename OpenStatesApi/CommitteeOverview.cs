using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenStatesApi
{
    public class CommitteeOverview
    {
        public string Id { get; set; }
        public State State { get; set; }
        public Chamber Chamber { get; set; }
        public string Committee { get; set; }
        public string Subcommittee { get; set; }
        [JsonProperty( "parent_id" )]
        public string ParentCommitteeId { get; set; }
        [JsonProperty( "created_at" )]
        public DateTime CreatedAt { get; set; }
        [JsonProperty( "updated_at" )]
        public DateTime UpdatedAt { get; set; }
    }
}
