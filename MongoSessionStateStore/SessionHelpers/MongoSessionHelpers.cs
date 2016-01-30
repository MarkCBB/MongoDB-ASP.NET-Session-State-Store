using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoSessionStateStore.SessionHelpers;
using System;
using System.Web;
using System.Web.SessionState;

namespace MongoSessionStateStore.SessionHelpers
{
    internal static class MongoSessionUserHelpers
    {
        static internal ISessionHelper __helper;

        static MongoSessionUserHelpers()
        {
            __helper = new SessionHelper();
        }

        internal static void SetHelper(ISessionHelper helper)
        {
            __helper = helper;
        }

        internal static T getObjValue<T>(object sessionObj)
        {
            return __helper.getObjValue<T>(sessionObj);
        }
    }
}

namespace System.Web.Mvc
{
    public static class MongoSessionUserHelpersMvc
    {
        public static void SetHelper(ISessionHelper helper)
        {
            MongoSessionUserHelpers.SetHelper(helper);
        }
        
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
            return MongoSessionUserHelpers.__helper.Mongo<T>(session, key);
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
            MongoSessionUserHelpers.__helper.Mongo<T>(session, key, newValue);
        }
    }
}

namespace System.Web
{
    public static class MongoSessionUserHelpersWeb
    {
        public static void SetHelper(ISessionHelper helper)
        {
            MongoSessionUserHelpers.SetHelper(helper);
        }
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
            return MongoSessionUserHelpers.__helper.Mongo<T>(session, key);
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
            MongoSessionUserHelpers.__helper.Mongo<T>(session, key, newValue);
        }
    }
}