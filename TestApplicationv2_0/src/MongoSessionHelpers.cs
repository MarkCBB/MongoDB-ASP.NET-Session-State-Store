using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MongoSessionHelpers
{
    public static class MongoSession
    {
        public static T GetMongoSession<T>(
            this Controller obj,
            string key)
        {
            var bsonVal = obj.Session[key] as BsonValue;
            if (bsonVal != null)
                return (T)BsonTypeMapper.MapToDotNetValue(bsonVal);

            var bsonDocument = obj.Session[key] as BsonDocument;
            if (bsonDocument != null)
                return (T)BsonSerializer.Deserialize<T>(bsonDocument);            

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