using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoSessionStateStore.SessionHelpers
{
    public class SessionHelper : ISessionHelper
    {
        public const string DECIMAL_EXCEPTION_MESSAGE = "Decimal types are not supported for serialization in Mongo.Session";

        public virtual T getObjValue<T>(object sessionObj)
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

        public virtual T Mongo<T>(System.Web.HttpSessionStateBase session, string key)
        {
            var sessionObj = session[key];
            return getObjValue<T>(sessionObj);
        }

        public virtual void Mongo<T>(System.Web.HttpSessionStateBase session, string key, T newValue)
        {
            var type = typeof(T);
            if ((type == typeof(decimal?)) || (type == typeof(decimal)))
                throw new Exception(DECIMAL_EXCEPTION_MESSAGE);

            if ((type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                && (Nullable.GetUnderlyingType(type).IsEnum))
                session[key] = newValue.ToString();
            else
                session[key] = newValue;
        }

        public virtual T Mongo<T>(System.Web.SessionState.HttpSessionState session, string key)
        {
            var sessionObj = session[key];
            return getObjValue<T>(sessionObj);
        }

        public virtual void Mongo<T>(System.Web.SessionState.HttpSessionState session, string key, T newValue)
        {
            var type = typeof(T);
            if ((type == typeof(decimal?)) || (type == typeof(decimal)))
                throw new Exception(DECIMAL_EXCEPTION_MESSAGE);

            if ((type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                && (Nullable.GetUnderlyingType(type).IsEnum))
                session[key] = newValue.ToString();
            else
                session[key] = newValue;
        }
    }
}
