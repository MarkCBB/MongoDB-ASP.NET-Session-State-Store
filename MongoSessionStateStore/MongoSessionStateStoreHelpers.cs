using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Threading.Tasks;
using MongoSessionStateStore.Serialization;

namespace MongoSessionStateStore
{
    internal static class MongoSessionStateStoreHelpers
    {
        internal static SerializationProxy __serializer;

        internal static void InitSerializationProxy(
            this MongoSessionStateStore obj, 
            SerializationProxy.SerializerType serializerType)
        {
            __serializer = new SerializationProxy(serializerType);
        }

        internal static string GetConfigVal(
            this MongoSessionStateStore obj,
            System.Collections.Specialized.NameValueCollection config,
            string parameterName)
        {
            var key = config.AllKeys.FirstOrDefault(x => string.Compare(x, parameterName, true) == 0);
            if ((!string.IsNullOrEmpty(key) && (!string.IsNullOrEmpty(config[key]))))
                return config[key];
            else
                return "";
        }

        internal static string GetDocumentSessionId(
            string sessionId,
            string applicationName)
        {
            return string.Format("{0}-{1}", sessionId, applicationName);
        }

        internal static BsonDocument GetNewBsonSessionDocument(
            string id,
            string applicationName,
            DateTime created,
            DateTime lockDate,
            int lockId,
            int timeout,
            bool locked,
            BsonArray bsonSessioNItemsArray,
            int flags = 1)
        {
            return new BsonDocument
                {
                    {"_id", GetDocumentSessionId(id, applicationName)},
                    {"SessionID", id},
                    {"ApplicationName", applicationName},
                    {"Created", created},
                    {"Expires", DateTime.Now.AddMinutes(timeout).ToUniversalTime()},
                    {"LockDate", lockDate},
                    {"LockId", lockId},
                    {"Timeout", timeout},
                    {"Locked", locked},
                    {"SessionItemBSON", bsonSessioNItemsArray},
                    {"Flags", flags}
                };
        }
        
        /// <summary>
        /// Creates TTL index if does not exist in collection.
        /// TTL index will remove the expired session documents.
        /// </summary>
        internal static bool CreateTTLIndex(
            IMongoCollection<BsonDocument> sessionCollection)
        {
            while (true)
            {
                try
                {
                    CreateIndexOptions c = new CreateIndexOptions();
                    c.ExpireAfter = TimeSpan.Zero;
                    c.Background = true;
                    string res = sessionCollection.Indexes.CreateOneAsync(
                        Builders<BsonDocument>.IndexKeys.Ascending("Expires"), c).Result;
                    return true;
                }
                catch (Exception)
                {
                    // if index is not created, not retries. App can continue without index but
                    // you should create it or clear the documents manually
                    return false;
                }
            }            
        }

        internal static BsonDocument FindOneSessionItem(
            this MongoSessionStateStore obj,
            IMongoCollection<BsonDocument> sessionCollection,
            FilterDefinition<BsonDocument> q)
        {
            int nAtempts = 0;
            while (true)
            {
                try
                {
                    return sessionCollection.Find(q).FirstOrDefaultAsync().Result;
                }
                catch (Exception e)
                {
                    PauseOrThrow(ref nAtempts, obj, sessionCollection, e);
                }
            }
        }

        internal static bool UpdateSessionCollection(
            this MongoSessionStateStore obj,
            IMongoCollection<BsonDocument> sessionCollection,
            FilterDefinition<BsonDocument> filter,
            UpdateDefinition<BsonDocument> update)
        {
            int attempts = 0;
            while (true)
            {
                try
                {
                    var result = sessionCollection.UpdateOneAsync(
                        filter,
                        update).Result;
                    return (result.ModifiedCount == 1);
                }
                catch (Exception e)
                {
                    PauseOrThrow(ref attempts, obj, sessionCollection, e);                    
                }
            }
        }

        internal static bool DeleteSessionDocument(
           this MongoSessionStateStore obj,
           IMongoCollection<BsonDocument> sessionCollection,
           FilterDefinition<BsonDocument> query)
        {
            int attempts = 0;
            while (true)
            {
                try
                {
                    return (sessionCollection.DeleteOneAsync(query).Result.DeletedCount == 1);
                }
                catch (Exception e)
                {
                    PauseOrThrow(ref attempts, obj, sessionCollection, e);                    
                }
            }
        }

        internal static void UpsertEntireSessionDocument(
            this MongoSessionStateStore obj,
            IMongoCollection<BsonDocument> sessionCollection,
            BsonDocument insertDoc)
        {
            int attempts = 0;
            while (true)
            {
                try
                {
                    Task.WaitAll(sessionCollection.InsertOneAsync(insertDoc));
                    return;
                }
                catch (Exception e)
                {
                    PauseOrThrow(ref attempts, obj, sessionCollection, e);                    
                }
            }
        }

        private static void PauseOrThrow(
            ref int attempts,
            MongoSessionStateStore obj,
            IMongoCollection<BsonDocument> sessionCollection,
            Exception e)
        {
            if (attempts < obj.MaxUpsertAttempts)
            {
                attempts++;
                System.Threading.Thread.CurrentThread.Join(obj.MsWaitingForAttempt);
            }
            else
            {
                throw new ProviderException(MongoSessionStateStore.EXCEPTION_MESSAGE);
            }
        }

        internal static void Serialize(
            this MongoSessionStateStore obj,
            SessionStateStoreData item,
            out BsonArray bsonArraySession)
        {
            bsonArraySession = __serializer.Serialize(item);
        }

        internal static SessionStateStoreData Deserialize(
            HttpContext context,
            BsonArray bsonSerializedItems,
            int timeout)
        {
            return __serializer.Deserialize(context, bsonSerializedItems, timeout);
        }

        
        /// <summary>
        /// NOT used. It's preserved for future implementations.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        /// <param name="action"></param>
        /// <param name="eventType"></param>
        internal static void WriteToEventLog(
            this MongoSessionStateStore obj,
            Exception e,
            string action,
            EventLogEntryType eventType = EventLogEntryType.Error)
        {
            if (obj.WriteExceptionsToEventLog)
            {
                using (var log = new EventLog())
                {
                    log.Source = MongoSessionStateStore.EVENT_SOURCE;
                    log.Log = MongoSessionStateStore.EVENT_LOG;

                    string message =
                      String.Format("An exception occurred communicating with the data source.\n\nAction: {0}\n\nException: {1}",
                      action, e);

                    log.WriteEntry(message, eventType);
                }
            }
        }
    }
}
