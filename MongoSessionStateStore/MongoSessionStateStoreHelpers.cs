using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace MongoSessionStateStore
{
    internal static class MongoSessionStateStoreHelpers
    {
        internal static BsonDocument GetNewBsonSessionDocument(
            this MongoSessionStateStore obj,
            string id,
            string applicationName,
            DateTime created,
            DateTime lockDate,
            int lockId,
            int timeout,
            bool locked,
            BsonArray jsonSessionItemsArray = null,
            int flags = 1)
        {
            return new BsonDocument
                {
                    {"_id", id},
                    {"ApplicationName", applicationName},
                    {"Created", created},
                    {"Expires", DateTime.Now.AddMinutes(timeout).ToUniversalTime()},
                    {"LockDate", lockDate},
                    {"LockId", lockId},
                    {"Timeout", timeout},
                    {"Locked", locked},
                    {"SessionItemJSON", jsonSessionItemsArray},
                    {"Flags", flags}
                };
        }
        
        /// <summary>
        /// Creates TTL index if does not exist in collection.
        /// TTL index will remove the expired session documents.
        /// </summary>
        internal static bool CreateTTLIndex(
            this MongoSessionStateStore obj,
            MongoCollection sessionCollection)
        {
            int nAttempts = 0;
            while (true)
            {
                try
                {
                    sessionCollection.CreateIndex(IndexKeys.Ascending("Expires"),
                                IndexOptions.SetTimeToLive(TimeSpan.Zero));
                    return true;
                }
                catch (Exception e)
                {
                    PauseOrThrow(ref nAttempts, obj, sessionCollection, e);
                }
            }            
        }

        internal static BsonDocument FindOneSessionItem(
            this MongoSessionStateStore obj,
            MongoCollection sessionCollection,
            IMongoQuery q)
        {
            int nAtempts = 0;
            while (true)
            {
                try
                {
                    return sessionCollection.FindOneAs<BsonDocument>(q);
                }
                catch (Exception e)
                {
                    PauseOrThrow(ref nAtempts, obj, sessionCollection, e);
                }
            }
        }

        internal static WriteConcernResult UpdateSessionCollection(
            this MongoSessionStateStore obj,
            MongoCollection sessionCollection,
            IMongoQuery query,
            UpdateBuilder update)
        {
            int attempts = 0;
            while (true)
            {
                try
                {
                    return sessionCollection.Update(query, update, obj.SessionWriteConcern);
                }
                catch (Exception e)
                {
                    PauseOrThrow(ref attempts, obj, sessionCollection, e);                    
                }
            }
        }

        internal static WriteConcernResult DeleteSessionDocument(
           this MongoSessionStateStore obj,
           MongoCollection sessionCollection,
           IMongoQuery query)
        {
            int attempts = 0;
            while (true)
            {
                try
                {
                    return sessionCollection.Remove(query, obj.SessionWriteConcern);
                }
                catch (Exception e)
                {
                    PauseOrThrow(ref attempts, obj, sessionCollection, e);                    
                }
            }
        }

        internal static WriteConcernResult UpsertEntireSessionDocument(
            this MongoSessionStateStore obj,
            MongoCollection sessionCollection,
            BsonDocument insertDoc)
        {
            int attempts = 0;
            while (true)
            {
                try
                {
                    return sessionCollection.Save(insertDoc.GetType(), insertDoc, obj.SessionWriteConcern);
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
            MongoCollection sessionCollection,
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

        internal static BsonArray Serialize(
            this MongoSessionStateStore obj,
            SessionStateStoreData item)
        {
            BsonArray arraySession = new BsonArray();
            for (int i = 0; i < item.Items.Count; i++)
            {
                string key = item.Items.Keys[i];
                arraySession.Add(new BsonDocument(key, Newtonsoft.Json.JsonConvert.SerializeObject(item.Items[key])));
            }
            return arraySession;
        }

        internal static SessionStateStoreData Deserialize(
            this MongoSessionStateStore obj,
            HttpContext context,
            BsonArray serializedItems,
            int timeout)
        {
            var jSonSessionItems = new SessionStateItemCollection();
            foreach (var value in serializedItems.Values)
            {
                var document = value as BsonDocument;
                string name = document.Names.FirstOrDefault();
                string JSonValues = document.Values.FirstOrDefault().AsString;
                jSonSessionItems[name] = Newtonsoft.Json.JsonConvert.DeserializeObject(JSonValues);
            }

            return new SessionStateStoreData(jSonSessionItems,
              SessionStateUtility.GetSessionStaticObjects(context),
              timeout);
        }
    }
}
