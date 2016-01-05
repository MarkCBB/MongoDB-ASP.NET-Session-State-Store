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
    internal class BsonSerialization : ISerialization
    {
        public BsonArray Serialize(SessionStateStoreData sessionData)
        {
            BsonArray bsonArraySession = new BsonArray();
            for (int i = 0; i < sessionData.Items.Count; i++)
            {
                string key = sessionData.Items.Keys[i];
                var sessionObj = sessionData.Items[key];
                if (sessionObj is BsonValue)
                {
                    bsonArraySession.Add(new BsonDocument(key, sessionObj as BsonValue));
                }
                else
                {
                    BsonValue singleValue;

                    if (BsonTypeMapper.TryMapToBsonValue(sessionObj, out singleValue))
                        bsonArraySession.Add(new BsonDocument(key, singleValue));
                    else
                        bsonArraySession.Add(new BsonDocument(key, sessionObj.ToBsonDocument()));
                }
            }
            return bsonArraySession;
        }

        public SessionStateStoreData Deserialize(
            HttpContext context,
            BsonArray bsonSerializedItems,
            int timeout)
        {
            var sessionItems = new SessionStateItemCollection();

            foreach (var value in bsonSerializedItems.Values)
            {
                var document = value as BsonDocument;
                string name = document.Names.FirstOrDefault();
                sessionItems[name] = document.Values.FirstOrDefault();
            }

            return new SessionStateStoreData(sessionItems,
              SessionStateUtility.GetSessionStaticObjects(context),
              timeout);
        }
    }
}
