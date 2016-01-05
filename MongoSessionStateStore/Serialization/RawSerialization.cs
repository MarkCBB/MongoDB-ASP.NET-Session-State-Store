using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace MongoSessionStateStore.Serialization
{
    internal class RawSerialization : ISerialization
    {
        public BsonArray Serialize(SessionStateStoreData item)
        {
            throw new NotImplementedException();
        }

        public SessionStateStoreData Deserialize(
            HttpContext context,
            BsonArray bsonSerializedItems, 
            int timeout)
        {
            throw new NotImplementedException();
        }
    }
}
