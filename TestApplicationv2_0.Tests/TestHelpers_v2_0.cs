using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication2_0.Tests
{
    public static class TestHelpers_v2_0
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

        public static string BASE_URL_FORMS = "http://localhost/TestApplicationv2_0/";
        public static string SET_VALUE_WEB_FORM = "WebFormTests/SetValues.aspx";
        public static string GET_VALUE_WEB_FORM = "WebFormTests/GetValues.aspx";

        public static string DEFAULT_WITH_HELPERS = "http://localhost/TestApplicationv2_0/DefaultWithHelpers/";
        public static string GET_AND_SET_SAME_REQUEST = "GetAndSetSameRequest/";
        public static string PRINT_SERIALIZED_PERSON = "PrintSessionSerializedPerson/";
        public static string PRINT_SERIALIZED_PERSON_WITH_LIST = "PrintSessionSerializedPersonWithlist/";
        public static string SET_SESSION_VAL_STRING = "SetSessionValString/";
        public static string PRINT_SESSION_VAL_STRING = "PrintSessionValString/";
        public static string SET_SESSION_VAL_DOUBLE_WITH_HELPERS = "SetSessionValDouble/";
        public static string PRINT_SESSION_VAL_DOUBLE = "PrintSessionValDouble/";
        public static string SET_SESSION_VAL_INT_WITH_HELPERS = "SetSessionValInt/";
        public static string PRINT_SESSION_VAL_INT = "PrintSessionValInt/";
        public static string SET_SESSION_VAL_BOOL_WITH_HELPERS = "SetSessionValBool/";
        public static string PRINT_SESSION_VAL_BOOL = "PrintSessionValBool/";

        public static string DEFAULT_WITH_BSON = "http://localhost/TestApplicationv2_0/BsonDocument/";
        public static string SET_BSON_VAL = "SetPerson/";
        public static string GET_BSON_VAL = "GetPerson/";

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
