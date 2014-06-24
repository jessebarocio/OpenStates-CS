using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStatesApi
{
    public class LegislatorOverview
    {
        [JsonProperty( "leg_id" )]
        public string LegislatorId { get; set; }
        public State State { get; set; }
        public bool Active { get; set; }
        public Chamber Chamber { get; set; }
        public string District { get; set; }
        public string Party { get; set; }
        public string Email { get; set; }
        [JsonProperty( "full_name" )]
        public string FullName { get; set; }
        [JsonProperty( "first_name" )]
        public string FirstName { get; set; }
        [JsonProperty( "middle_name" )]
        public string MiddleName { get; set; }
        [JsonProperty( "last_name" )]
        public string LastName { get; set; }
        public string Suffixes { get; set; }
        [JsonProperty( "photo_url" )]
        public string PhotoUrl { get; set; }
        public string Url { get; set; }
        [JsonProperty( "created_at" )]
        public DateTime CreatedAt { get; set; }
        [JsonProperty( "updated_at" )]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty( "transparencydata_id" )]
        public string TransparencyDataId { get; set; }
        public IEnumerable<Office> Offices { get; set; }
    }
}
