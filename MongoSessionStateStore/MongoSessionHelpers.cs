using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Web;

namespace MongoSessionStateStore.Helpers
{
    public static class MongoSessionUserHelpers
    {
        /// <summary>
        /// Gets the session value stored in MongoDB.
        /// </summary>
        /// <typeparam name="T">Type of the value to get.</typeparam>
        /// <param name="session">HttpSessionStateBase object for MVC pages.</param>
        /// <param name="key">The session key name.</param>
        /// <returns>The object requested. Null if not exists.</returns>
        public static T Mongo<T>(
            this HttpSessionStateBase session,
            string key)
        {
            var sessionObj = session[key];
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

        /// <summary>
        /// Sets the session value to store in MongoDB.
        /// </summary>
        /// <typeparam name="T">Type of value to store.</typeparam>
        /// <param name="session">HttpSessionStateBase object for MVC pages.</param>
        /// <param name="key">The session key name.</param>
        /// <param name="newValue">The value to store.</param>
        public static void Mongo<T>(
            this HttpSessionStateBase session,
            string key,
            T newValue)
        {
            session[key] = newValue;
        }        
    }
}