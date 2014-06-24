using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStatesApi
{
    public class District
    {
        [JsonProperty( "abbr" )]
        public State State { get; set; }
        [JsonProperty( "boundary_id" )]
        public string BoundaryId { get; set; }
        public Chamber Chamber { get; set; }
        public IEnumerable<DistrictLegislatorDetail> Legislators { get; set; }
        public string Name { get; set; }
        [JsonProperty( "num_seats" )]
        public int NumberOfSeats { get; set; }
    }

    public class DistrictLegislatorDetail
    {
        [JsonProperty( "leg_id" )]
        public string LegislatorId { get; set; }
        [JsonProperty( "full_name" )]
        public string FullName { get; set; }
    }
}
