using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Web.Mvc;

namespace MongoSessionStateStore.Helpers
{
    public static class MongoSessionUserHelpers
    {
        public static T GetMongoSession<T>(
            this Controller obj,
            string key)
        {
            var sessionObj = obj.Session[key];
            if (sessionObj == null)
                return default(T);

            if (sessionObj is T)
                return (T)sessionObj;

            if (sessionObj is BsonValue)
                return (T)BsonTypeMapper.MapToDotNetValue(sessionObj as BsonValue);

            if (sessionObj is BsonDocument)
                return (T)BsonSerializer.Deserialize<T>(sessionObj as BsonDocument);

            return default(T);
        }

        public static void SetMongoSession<T>(
            this Controller obj,
            string key,
            T value)
        {
            obj.Session[key] = value;
        }        
    }
}