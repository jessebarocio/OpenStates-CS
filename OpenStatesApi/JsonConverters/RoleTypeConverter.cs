using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStatesApi.JsonConverters
{
    public class RoleTypeConverter : JsonConverter
    {

        public override bool CanConvert( Type objectType )
        {
            return objectType == typeof( RoleType );
        }

        public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer )
        {
            string roleType = reader.Value as string;
            switch ( roleType.ToLower() )
            {
                case "member":
                    return RoleType.Member;
                case "committee member":
                    return RoleType.CommitteeMember;
                default:
                    return null;
            }
        }

        public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer )
        {
            throw new NotImplementedException( "Write is not implemented on these custom converters." );
        }
    }
}
