using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Web;
using System.Web.SessionState;

namespace MongoSessionStateStore.Helpers
{
    public static class MongoSessionUserHelpers
    {
        public static string DECIMAL_EXCEPTION_MESSAGE = "Decimal types are not supported for serialization in Mongo.Session";

        public static T getObjValue<T>(object sessionObj)
        {
            if (sessionObj == null)
                return default(T);

            if (sessionObj is T)
                return (T)sessionObj;
            
            if (sessionObj is BsonDocument)
                return (T)BsonSerializer.Deserialize<T>(sessionObj as BsonDocument);

            var type = typeof(T);

            if ((type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                && (Nullable.GetUnderlyingType(type).IsEnum))
            {
                if ((sessionObj == null) || (string.IsNullOrEmpty(sessionObj.ToString())))
                    return default(T);

                BsonValue bsonObject = sessionObj as BsonValue;
                if (bsonObject != null)
                    return (T)Enum.Parse(
                        Nullable.GetUnderlyingType(type),
                        (string)BsonTypeMapper.MapToDotNetValue(bsonObject));
                else
                    return (T)Enum.Parse(
                        Nullable.GetUnderlyingType(type),
                        (string)sessionObj);
            }

            if (sessionObj is BsonValue)     
                return (T)BsonTypeMapper.MapToDotNetValue(sessionObj as BsonValue);

            return default(T);
        }
    }
}

namespace System.Web.Mvc
{
    public static class MongoSessionUserHelpersMvc
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
            return MongoSessionStateStore.Helpers.MongoSessionUserHelpers.getObjValue<T>(sessionObj);
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
            var type = typeof(T);
            if ((type == typeof(decimal?)) || (type == typeof(decimal)))
                throw new Exception(MongoSessionStateStore.Helpers.MongoSessionUserHelpers.DECIMAL_EXCEPTION_MESSAGE);

            if ((type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                && (Nullable.GetUnderlyingType(type).IsEnum))
                session[key] = newValue.ToString();
            else
                session[key] = newValue;
        }
    }
}

namespace System.Web
{
    public static class MongoSessionUserHelpersWeb
    {
        /// <summary>
        /// Gets the session value stored in MongoDB.
        /// </summary>
        /// <typeparam name="T">Type of the value to get.</typeparam>
        /// <param name="session">HttpSessionState object for WebForm pages.</param>
        /// <param name="key">The session key name.</param>
        /// <returns>The object requested. Null if not exists.</returns>
        public static T Mongo<T>(
            this HttpSessionState session,
            string key)
        {
            var sessionObj = session[key];
            return MongoSessionStateStore.Helpers.MongoSessionUserHelpers.getObjValue<T>(sessionObj);
        }

        /// <summary>
        /// Sets the session value to store in MongoDB.
        /// </summary>
        /// <typeparam name="T">Type of value to store.</typeparam>
        /// <param name="session">HttpSessionState object for WebForm pages.</param>
        /// <param name="key">The session key name.</param>
        /// <param name="newValue">The value to store.</param>
        public static void Mongo<T>(
            this HttpSessionState session,
            string key,
            T newValue)
        {
            var type = typeof(T);
            if ((type == typeof(decimal?)) || (type == typeof(decimal)))
                throw new Exception(MongoSessionStateStore.Helpers.MongoSessionUserHelpers.DECIMAL_EXCEPTION_MESSAGE);

            if ((type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                && (Nullable.GetUnderlyingType(type).IsEnum))
                session[key] = newValue.ToString();
            else
                session[key] = newValue;
        }
    }
}