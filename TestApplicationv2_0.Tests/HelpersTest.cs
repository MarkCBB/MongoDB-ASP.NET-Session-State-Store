using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using TestApplication2_0.Tests;
using System.Threading.Tasks;

namespace TestApplicationv2_0.Tests
{
    [TestClass]
    public class HelpersTest
    {
        [TestMethod]
        public void GetAndSetSameRequest(CookieContainer cookieContainer = null)
        {
            cookieContainer = cookieContainer ?? new CookieContainer();
            string url = TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.GET_AND_SET_SAME_REQUEST;
            string result = TestHelpers_v2_0.DoRequest(url, cookieContainer);
            StringAssert.Contains(result, "<result>True</result>");
        }

        [TestMethod]
        public void PrintSessionSerializedPerson()
        {
            CookieContainer cookieContainer = new CookieContainer();
            GetAndSetSameRequest(cookieContainer);
            string url = TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.PRINT_SERIALIZED_PERSON;
            string result = TestHelpers_v2_0.DoRequest(url, cookieContainer);
            StringAssert.Contains(result, @"<fieldset>
        <legend>Person</legend>
    
        <div class=""display-label"">
            Name
        </div>
        <div class=""display-field"">
            Marc
        </div>
    
        <div class=""display-label"">
            Surname
        </div>
        <div class=""display-field"">
            Cortada
        </div>
    
        <div class=""display-label"">
            City
        </div>
        <div class=""display-field"">
            Barcelona
        </div>
    </fieldset>");
        }

        [TestMethod]
        public void PrintSessionSerializedPersonWithlist()
        {
            CookieContainer cookieContainer = new CookieContainer();
            GetAndSetSameRequest(cookieContainer);            
            string url = 
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.PRINT_SERIALIZED_PERSON_WITH_LIST;
            string result = TestHelpers_v2_0.DoRequest(url, cookieContainer);

            StringAssert.Contains(result, @"<fieldset>
        <legend>PersonPetsList</legend>

        <div class=""display-label"">
            Name
        </div>
        <div class=""display-field"">
            Marc2
        </div>

        <div class=""display-label"">
            Surname
        </div>
        <div class=""display-field"">
            Cortada2
        </div>

        <div class=""display-label"">
            City
        </div>
        <div class=""display-field"">
            
        </div>
        <div class=""display-field"">
            
        </div>        
        
        <div class=""display-field"">
            cat
        </div>
        
        <div class=""display-field"">
            dog
        </div>
        
    </fieldset>");
        }

        [TestMethod]
        public void SetSessionValString()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string url =
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.SET_SESSION_VAL_STRING +
                "hola",
                url2 = TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.PRINT_SESSION_VAL_STRING;
            TestHelpers_v2_0.DoRequest(url, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(result, "<sessionVal>hola</sessionVal>");
        }

        [TestMethod]
        public void SessionValDouble()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string url =
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.SET_SESSION_VAL_DOUBLE_WITH_HELPERS,
                url2 =
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.PRINT_SESSION_VAL_DOUBLE;
            TestHelpers_v2_0.DoRequest(url, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(result, "<sessionVal>3,1416</sessionVal>");
        }

        [TestMethod]
        public void SessionValInt()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string url = 
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.SET_SESSION_VAL_INT_WITH_HELPERS + 17,
                url2 = 
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.PRINT_SESSION_VAL_INT;
            TestHelpers_v2_0.DoRequest(url, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(result, "<sessionVal>17</sessionVal>");
        }

        [TestMethod]
        public void SessionValBool()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string url = 
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.SET_SESSION_VAL_BOOL_WITH_HELPERS,
                url2 = 
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.PRINT_SESSION_VAL_BOOL;
            TestHelpers_v2_0.DoRequest(url, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(result, "<sessionVal>False</sessionVal>");
        }

        [TestMethod]
        public void SetEnumValue()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string url = 
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.SET_SESSION_VAL_ENUM_VALUE_WITH_HELPERS,
                url2 = 
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.PRINT_SESSION_VAL_ENUM_VALUE;
            string resultSet = TestHelpers_v2_0.DoRequest(url, cookieContainer);
            StringAssert.Contains(resultSet, "<sessionVal>Black</sessionVal>");
            string resultGet = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(resultGet, "<sessionVal>Black</sessionVal>");
        }

        [TestMethod]
        public void SetEnumValueNullable()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string url = 
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.SET_SESSION_VAL_ENUM_NULLABLE_VALUE_WITH_HELPERS,
                url2 = 
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.PRINT_SESSION_VAL_ENUM_NULLABLE_VALUE;
            string resultSet = TestHelpers_v2_0.DoRequest(url, cookieContainer);
            StringAssert.Contains(resultSet, "<sessionVal>DarkBlue</sessionVal>");
            string resultGet = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(resultGet, "<sessionVal>DarkBlue</sessionVal>");
        }

        [TestMethod]
        public void SetEnumValueNull()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string url = 
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.SET_SESSION_VAL_ENUM_NULL_VALUE_WITH_HELPERS,
                url2 = 
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.PRINT_SESSION_VAL_ENUM_NULL_VALUE;
            string resultSet = TestHelpers_v2_0.DoRequest(url, cookieContainer);
            StringAssert.Contains(resultSet, "<sessionVal>OK</sessionVal>");
            string resultGet = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(resultGet, "<sessionVal>OK</sessionVal>");
        }

        [TestMethod]
        public void SetNullPerson()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string url = 
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.SET_SESSION_VAL_PERSON_NULL_VALUE_WITH_HELPERS,
                url2 = 
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.PRINT_SESSION_VAL_PERSON_NULL_VALUE;
            string resultSet = TestHelpers_v2_0.DoRequest(url, cookieContainer);
            StringAssert.Contains(resultSet, "<sessionVal>OK</sessionVal>");
            string resultGet = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(resultGet, "<sessionVal>OK</sessionVal>");
        }

        [TestMethod]
        public void SetNullableInt()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string url = 
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.SET_SESSION_VAL_NULLABLE_INT_VALUE_WITH_HELPERS,
                url2 = 
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.PRINT_SESSION_VAL_NULLABLE_INT_VALUE;
            string resultSet = TestHelpers_v2_0.DoRequest(url, cookieContainer);
            StringAssert.Contains(resultSet, "<sessionVal>3</sessionVal>");
            string resultGet = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(resultGet, "<sessionVal>3</sessionVal>");
        }

        [TestMethod]
        public void SetNullableIntToNull()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string url = 
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.SET_SESSION_VAL_NULLABLE_INT_NULL_VALUE_WITH_HELPERS,
                url2 = 
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.PRINT_SESSION_VAL_NULLABLE_INT_NULL_VALUE;
            string resultSet = TestHelpers_v2_0.DoRequest(url, cookieContainer);
            StringAssert.Contains(resultSet, "<sessionVal>3</sessionVal>");
            string resultGet = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(resultGet, "<sessionVal>3</sessionVal>");
        }

        [TestMethod]
        public void SetValueDecimal()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string url =
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.SET_SESSION_VAL_DECIMAL_VALUE_WITH_HELPERS,
                url2 =
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.PRINT_SESSION_VAL_DECIMAL_VALUE;
            string resultSet = TestHelpers_v2_0.DoRequest(url, cookieContainer);
            StringAssert.Contains(resultSet, "<sessionVal>OK</sessionVal>");
            string resultGet = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(resultGet, "<sessionVal>OK</sessionVal>");
        }

        [TestMethod]
        public void SetNullableValueDecimal()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string url =
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.SET_SESSION_VAL_NULLABLE_DECIMAL_WITH_HELPERS,
                url2 =
                    TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                    TestHelpers_v2_0.PRINT_SESSION_VAL_NULLABLE_DECIMAL_VALUE_WITH_HELPERS;
            string resultSet = TestHelpers_v2_0.DoRequest(url, cookieContainer);
            StringAssert.Contains(resultSet, "<sessionVal>OK</sessionVal>");
            string resultGet = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(resultGet, "<sessionVal>OK</sessionVal>");
        }

        [TestMethod]
        public async Task Long_Processes_Can_Write_After_Another_Reads_A_Value()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string shortWriteProcessUrl = TestHelpers_v2_0.DEFAULT_WITH_HELPERS + "ShortTimeWriteProcess";
            string longWriteProcessUrl = TestHelpers_v2_0.DEFAULT_WITH_HELPERS + "LongTimeWriteProcess";
            string shortReadUrl = TestHelpers_v2_0.DEFAULT_WITH_HELPERS_READ_ONLY_SESSION_STATE + "ReadLongRunningValueProcess";

            // Populate session id in cookieContainer.
            await TestHelpers_v2_0.DoRequestAsync(shortWriteProcessUrl, cookieContainer);

            var longWriteTask = TestHelpers_v2_0.DoRequestAsync(longWriteProcessUrl, cookieContainer);

            await Task.Delay(5000);

            // This will increment lockId preventing longWriteTask from writing session object to database.
            await TestHelpers_v2_0.DoRequestAsync(shortReadUrl, cookieContainer);

            await longWriteTask;

            var sessionVal = await TestHelpers_v2_0.DoRequestAsync(shortReadUrl, cookieContainer);

            StringAssert.Contains(sessionVal, "<sessionVal>30</sessionVal>");
        }
    }
}
