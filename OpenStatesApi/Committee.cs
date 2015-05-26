using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenStatesApi
{
    public class Committee
    {
        [JsonProperty("committee")]
        public string Name { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("parent_id")]
        public string ParentCommitteeId { get; set; }
        public State State { get; set; }
        public string Subcommittee { get; set; }
        public Chamber Chamber { get; set; }
        public IEnumerable<CommitteeMember> Members { get; set; }
        public string Id { get; set; }
        [JsonProperty("votesmart_id")]
        public string VotesmartId { get; set; }
    }
}
