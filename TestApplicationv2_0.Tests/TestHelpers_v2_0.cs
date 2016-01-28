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
        public static string SET_SESSION_VAL_ENUM_VALUE_WITH_HELPERS = "SetSessionValEnum/";
        public static string SET_SESSION_VAL_ENUM_NULLABLE_VALUE_WITH_HELPERS = "SetSessionValNullableEnum/";
        public static string SET_SESSION_VAL_ENUM_NULL_VALUE_WITH_HELPERS = "SetSessionValNullableEnumToNull/";
        public static string SET_SESSION_VAL_PERSON_NULL_VALUE_WITH_HELPERS = "SetNullPersonValue/";
        public static string SET_SESSION_VAL_NULLABLE_INT_VALUE_WITH_HELPERS = "SetSessionValNullableInt/";
        public static string SET_SESSION_VAL_NULLABLE_INT_NULL_VALUE_WITH_HELPERS = "SetSessionValNullableInt/";
        public static string SET_SESSION_VAL_NULLABLE_DECINAL_NULL_VALUE_WITH_HELPERS = "SetNullableDecimalToNull/";
        public static string SET_SESSION_VAL_NULLABLE_DECINAL_VALUE_WITH_HELPERS = "SetSessionValNullableDecimal/";
        public static string PRINT_SESSION_VAL_BOOL = "PrintSessionValBool/";
        public static string PRINT_SESSION_VAL_ENUM_VALUE = "GetSessionValEnum/";
        public static string PRINT_SESSION_VAL_ENUM_NULLABLE_VALUE = "GetSessionValNullableEnum/";
        public static string PRINT_SESSION_VAL_ENUM_NULL_VALUE = "GetSessionValNullableEnumToNull/";
        public static string PRINT_SESSION_VAL_PERSON_NULL_VALUE = "GetNullPersonValue/";
        public static string PRINT_SESSION_VAL_NULLABLE_INT_VALUE = "GetSessionValNullableInt/";
        public static string PRINT_SESSION_VAL_NULLABLE_INT_NULL_VALUE = "GetSessionValNullableInt/";
        public static string PRINT_SESSION_VAL_NULLABLE_DECIMAL_NULL_VALUE = "GetNullableDecimalToNull/";
        public static string PRINT_SESSION_VAL_NULLABLE_DECIMAL_VALUE = "GetSessionValNullableDecimal/";

        public static string DEFAULT_WITH_BSON = "http://localhost/TestApplicationv2_0/BsonDocument/";
        public static string SET_BSON_VAL = "SetPerson/";
        public static string GET_BSON_VAL = "GetPerson/";

        public static string DoRequest(
            string url,
            CookieContainer cookieContainer)
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cookieContainer;
            var stream = request.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
