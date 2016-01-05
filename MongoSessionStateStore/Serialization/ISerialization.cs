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
    internal interface ISerialization
    {
        BsonArray Serialize(SessionStateStoreData item);
        SessionStateStoreData Deserialize(HttpContext context, BsonArray bsonSerializedItems, int timeout);
    }
}
