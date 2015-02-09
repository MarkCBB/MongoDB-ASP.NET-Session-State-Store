using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MongoSessionStateStore
{
    internal static class MongoSessionStateStoreHelpers
    {
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
                catch (MongoConnectionException)
                {
                    // Exception thrown means inserted or updated document
                    if (attempts < obj.MaxUpsertAttempts)
                    {
                        attempts++;
                        System.Threading.Thread.CurrentThread.Join(obj.MsWaitingForAttempt);
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception e)
                {
                    if (obj.WriteExceptionsToEventLog)
                    {
                        WriteToEventLog(e, "SetAndReleaseItemExclusive");
                        throw new ProviderException(MongoSessionStateStore.ExceptionMessage);
                    }
                    throw;
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
                catch (MongoConnectionException)
                {
                    // Exception thrown means inserted or updated document
                    if (attempts < obj.MaxUpsertAttempts)
                    {
                        attempts++;
                        System.Threading.Thread.CurrentThread.Join(obj.MaxUpsertAttempts);
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception e)
                {
                    if (obj.WriteExceptionsToEventLog)
                    {
                        WriteToEventLog(e, "SetAndReleaseItemExclusive");
                        throw new ProviderException(MongoSessionStateStore.ExceptionMessage);
                    }
                    throw;
                }
            }
        }

        internal static WriteConcernResult UpsertDocument(
            this MongoSessionStateStore obj,
            MongoCollection sessionCollection,
            BsonDocument insertDoc)
        {
            int attempts = 0;
            while (true)
            {
                try
                {
                    return sessionCollection.Save(insertDoc.BsonType.GetType(), insertDoc, obj.SessionWriteConcern);
                }
                catch (MongoConnectionException)
                {
                    // Exception thrown means inserted or updated document
                    if (attempts < obj.MaxUpsertAttempts)
                    {
                        attempts++;
                        System.Threading.Thread.CurrentThread.Join(obj.MsWaitingForAttempt);
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception e)
                {
                    if (obj.WriteExceptionsToEventLog)
                    {
                        WriteToEventLog(e, "SetAndReleaseItemExclusive");
                        throw new ProviderException(MongoSessionStateStore.ExceptionMessage);
                    }
                    throw;
                }
            }
        }

        /// <summary>
        /// This is a helper function that writes exception detail to the 
        /// event log. Exceptions are written to the event log as a security
        /// measure to ensure private database details are not returned to 
        /// browser. If a method does not return a status or Boolean
        /// indicating the action succeeded or failed, the caller also 
        /// throws a generic exception.
        /// </summary>
        private static void WriteToEventLog(Exception e, string action)
        {
            using (var log = new EventLog())
            {
                log.Source = MongoSessionStateStore.EventSource;
                log.Log = MongoSessionStateStore.EventLog;

                string message =
                  String.Format("An exception occurred communicating with the data source.\n\nAction: {0}\n\nException: {1}",
                  action, e);

                log.WriteEntry(message);
            }
        }
    }
}
