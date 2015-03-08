using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication2_0.Tests
{
    public static class TestHelpers
    {
        public static string BASE_URL = "http://localhost/TestApplicationv2_0/Default/";
        public static string PRINT_SESION_ACTION = "PrintSessionVal/";
        public static string PRINT_SESION_DOUBLE = "PrintSessionValDouble/";
        public static string SET_SESSION_ACTION = "SetSessionVal/";
        public static string SESSION_ABANDON_ACTION = "SessionAbandon/";
        public static string SET_SESSION_VAL_INT = "SetSessionValInt/";
        public static string SET_SESSION_VAL_BOOL = "SetSessionValBool/";
        public static string SET_SESSION_VAL_DOUBLE = "SetSessionValDouble/";
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
