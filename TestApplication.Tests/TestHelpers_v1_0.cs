using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.Tests
{
    public static class TestHelpers_v1_0
    {
        public static string BASE_URL = "http://localhost/TestApplication/Default/";
        public static string PRINT_SESION_ACTION = "PrintSessionVal/";
        public static string SET_SESSION_ACTION = "SetSessionVal/";
        public static string SESSION_ABANDON_ACTION = "SessionAbandon/";
        public static string SET_SESSION_VAL_INT = "SetSessionValInt/";
        public static string SET_SESSION_VAL_BOOL = "SetSessionValBool/";
        public static string SET_SESSION_VAL_DECIMAL = "SetSessionValDecimal/";
        public static string SET_SESSION_VAL_JSON_SERIALIZEPERSON = "SerializePerson/";
        public static string PRINT_SESSION_VAL_JSON_SERIALIZEPERSON = "GetSerializedPerson/";
        public static string SET_SESSION_VAL_JSON_SERIALIZELIST = "SerializePersonWithLists/";
        public static string PRINT_SESSION_VAL_JSON_SERIALIZELIST = "GetSerializedPersonWithPets/";

        public static string DoRequest(
            HttpWebRequest request,
            CookieContainer cookieContainer)
        {
            request.CookieContainer = cookieContainer;
            var stream = request.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
