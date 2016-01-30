using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace MongoSessionStateStore.SessionHelpers
{
    public interface ISessionHelper
    {
        T getObjValue<T>(object sessionObj);
        T Mongo<T>(HttpSessionStateBase session, string key);
        void Mongo<T>(HttpSessionStateBase session, string key, T newValue);
        T Mongo<T>(HttpSessionState session, string key);
        void Mongo<T>(HttpSessionState session, string key, T newValue);

    }
}
