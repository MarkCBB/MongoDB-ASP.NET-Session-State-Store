using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using TestApplication2_0.Tests;

namespace TestApplicationv2_0.Tests
{
    [TestClass]
    public class HelpersTest
    {
        [TestMethod]
        public void GetAndSetSameRequest(CookieContainer cookieContainer = null)
        {
            cookieContainer = cookieContainer ?? new CookieContainer();
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.GET_AND_SET_SAME_REQUEST);
            string result = TestHelpers_v2_0.DoRequest(request, cookieContainer);
            StringAssert.Contains(result, "<result>True</result>");
        }

        [TestMethod]
        public void PrintSessionSerializedPerson()
        {
            CookieContainer cookieContainer = new CookieContainer();
            GetAndSetSameRequest(cookieContainer);            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.PRINT_SERIALIZED_PERSON);
            string result = TestHelpers_v2_0.DoRequest(request, cookieContainer);
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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.PRINT_SERIALIZED_PERSON_WITH_LIST);
            string result = TestHelpers_v2_0.DoRequest(request, cookieContainer);

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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.SET_SESSION_VAL_STRING +
                "hola"),
                request2 = (HttpWebRequest)WebRequest.Create(
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.PRINT_SESSION_VAL_STRING);
            TestHelpers_v2_0.DoRequest(request, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(request2, cookieContainer);
            StringAssert.Contains(result, "<sessionVal>hola</sessionVal>");
        }

        [TestMethod]
        public void SessionValDouble()
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.SET_SESSION_VAL_DOUBLE_WITH_HELPERS),
                request2 = (HttpWebRequest)WebRequest.Create(
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.PRINT_SESSION_VAL_DOUBLE);
            TestHelpers_v2_0.DoRequest(request, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(request2, cookieContainer);
            StringAssert.Contains(result, "<sessionVal>3,1416</sessionVal>");
        }

        [TestMethod]
        public void SessionValInt()
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.SET_SESSION_VAL_INT_WITH_HELPERS + 17),
                request2 = (HttpWebRequest)WebRequest.Create(
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.PRINT_SESSION_VAL_INT);
            TestHelpers_v2_0.DoRequest(request, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(request2, cookieContainer);
            StringAssert.Contains(result, "<sessionVal>17</sessionVal>");
        }

        [TestMethod]
        public void SessionValBool()
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.SET_SESSION_VAL_BOOL_WITH_HELPERS),
                request2 = (HttpWebRequest)WebRequest.Create(
                TestHelpers_v2_0.DEFAULT_WITH_HELPERS +
                TestHelpers_v2_0.PRINT_SESSION_VAL_BOOL);
            TestHelpers_v2_0.DoRequest(request, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(request2, cookieContainer);
            StringAssert.Contains(result, "<sessionVal>False</sessionVal>");
        }
    }
}
