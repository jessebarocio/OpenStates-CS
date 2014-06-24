using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStatesApi.JsonConverters
{
    public class DistrictPolygonConverter : JsonConverter
    {
        public override bool CanConvert( Type objectType )
        {
            return objectType == typeof( DistrictPolygon );
        }

        public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer )
        {
            var polygons = new List<DistrictPolygon>();
            reader.Read(); // start array
            var array = (JArray)serializer.Deserialize( reader );
            foreach ( JArray polygon in array )
            {
                var item = new DistrictPolygon();
                foreach ( JArray point in polygon )
                {
                    item.Points.Add( new Point()
                        {
                            Latitude = point[0].Value<double>(),
                            Longitude = point[1].Value<double>()
                        } );
                }
                polygons.Add( item );
            }
            reader.Read(); // end array
            return polygons;
        }

        public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer )
        {
            throw new NotImplementedException( "Write is not implemented on these custom converters." );
        }
    }
}
