using Newtonsoft.Json;
using OpenStatesApi.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStatesApi
{
    public class DistrictBoundary
    {
        [JsonProperty( "abbr" )]
        public State State { get; set; }
        [JsonProperty( "boundary_id" )]
        public string BoundaryId { get; set; }
        public Chamber Chamber { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonProperty( "num_seats" )]
        public int NumberOfSeats { get; set; }
        public DistrictBoundaryRegion Region { get; set; }
        [JsonConverter( typeof( DistrictPolygonConverter ) )]
        public IEnumerable<DistrictPolygon> Shape { get; set; }
    }

    public class DistrictBoundaryRegion
    {
        [JsonProperty( "center_lat" )]
        public double CenterLatitude { get; set; }
        [JsonProperty( "center_lon" )]
        public double CenterLongitude { get; set; }
        [JsonProperty( "lat_delta" )]
        public double LatitudeDelta { get; set; }
        [JsonProperty( "lon_delta" )]
        public double LongitudeDelta { get; set; }
    }

    public class DistrictPolygon
    {
        public IList<Point> Points { get; set; }

        public DistrictPolygon()
        {
            Points = new List<Point>();
        }
    }

    public class Point
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
