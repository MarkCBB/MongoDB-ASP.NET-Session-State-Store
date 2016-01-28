using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;


namespace TestApplication2_0.Tests
{
    [TestClass]
    public class SimplyGetAndSet_v2_0
    {
        /// <summary>
        /// This test case does a request without value assignation, so a blank string is expected
        /// </summary>
        [TestMethod]
        public void InitBlankValue()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string url =
                TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.PRINT_SESION_ACTION;
            string result = TestHelpers_v2_0.DoRequest(url, cookieContainer);
            // Blank value is expected
            StringAssert.Contains(result, string.Format("<sessionVal>{0}</sessionVal>", ""));            
        }

        /// <summary>
        /// This test case assigns a value to a SessionState 
        /// </summary>
        [TestMethod]
        public void SingleSetValueString()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string textToSet = "valueSettedInSession";
            string url = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.SET_SESSION_ACTION + textToSet,
                url2 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.PRINT_SESION_ACTION;
            TestHelpers_v2_0.DoRequest(url, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(result, string.Format("<sessionVal>{0}</sessionVal>", textToSet));
        }

        /// <summary>
        /// This test case assigns a value twice with two requests.
        /// </summary>
        [TestMethod]
        public void SetValueStringTwice()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string textToSet1 = "valueSettedInSession", textToSet2 = "valueSettedInSession2";
            string url = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.SET_SESSION_ACTION + textToSet1,
                url2 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.SET_SESSION_ACTION + textToSet2,
                url3 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.PRINT_SESION_ACTION;
            TestHelpers_v2_0.DoRequest(url, cookieContainer);
            TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(url3, cookieContainer);
            StringAssert.Contains(result, string.Format("<sessionVal>{0}</sessionVal>", textToSet2));
        }

        /// <summary>
        /// This test case assigns a value to a SessionState 
        /// </summary>
        [TestMethod]
        public void SingleSetValueInt()
        {
            CookieContainer cookieContainer = new CookieContainer();
            int intToSet = 1;
            string url = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.SET_SESSION_VAL_INT + intToSet,
                url2 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.PRINT_SESION_ACTION;
            TestHelpers_v2_0.DoRequest(url, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(result, string.Format("<sessionVal>{0}</sessionVal>", intToSet));
        }

        /// <summary>
        /// This test case assigns a value to a SessionState 
        /// </summary>
        [TestMethod]
        public void SingleSetValueFloat()
        {
            double sesVal = 3.1416F;
            CookieContainer cookieContainer = new CookieContainer();
            string url = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.SET_SESSION_VAL_DOUBLE,
                url2 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.PRINT_SESION_DOUBLE;
            TestHelpers_v2_0.DoRequest(url, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(result, string.Format("<sessionVal>{0}</sessionVal>", sesVal.ToString("G")));
        }

        /// <summary>
        /// This test case assigns a value to a SessionState 
        /// </summary>
        [TestMethod]
        public void SingleSetValueBool()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string url1 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.SET_SESSION_VAL_BOOL + true,
                url2 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.PRINT_SESION_ACTION,
                url3 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.SET_SESSION_VAL_BOOL + false,
                url4 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.PRINT_SESION_ACTION;
            
            //Trying setting with true
            TestHelpers_v2_0.DoRequest(url1, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(result.ToUpper(), string.Format("<sessionVal>{0}</sessionVal>", "True").ToUpper());

            //Trying setting with false
            TestHelpers_v2_0.DoRequest(url3, cookieContainer);
            result = TestHelpers_v2_0.DoRequest(url4, cookieContainer);
            StringAssert.Contains(result.ToUpper(), string.Format("<sessionVal>{0}</sessionVal>", "False").ToUpper());
        }

        /// <summary>
        /// This test case assigns a value to a SessionState twice
        /// </summary>
        [TestMethod]
        public void SingleValueTwice()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string textToSet1 = "valueSettedInSession", textToSet2 = "Second valueSetted In Session state";
            string url1 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.SET_SESSION_ACTION + textToSet1,
                url2 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.PRINT_SESION_ACTION,
                url3 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.SET_SESSION_ACTION + textToSet2,
                url4 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.PRINT_SESION_ACTION;
            TestHelpers_v2_0.DoRequest(url1, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(result, string.Format("<sessionVal>{0}</sessionVal>", textToSet1));
            TestHelpers_v2_0.DoRequest(url3, cookieContainer);
            result = TestHelpers_v2_0.DoRequest(url4, cookieContainer);
            StringAssert.Contains(result, string.Format("<sessionVal>{0}</sessionVal>", textToSet2));
        }

        /// <summary>
        /// This test case assigns a value to a SessionState twice
        /// </summary>
        [TestMethod]
        public void SessionAbandon()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string textToSet1 = "valueSettedInSession";
            string url1 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.SET_SESSION_ACTION + textToSet1,
                url2 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.PRINT_SESION_ACTION,
                url3 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.SESSION_ABANDON_ACTION,
                url4 = TestHelpers_v2_0.BASE_URL + TestHelpers_v2_0.PRINT_SESION_ACTION;
            TestHelpers_v2_0.DoRequest(url1, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            Assert.IsTrue(result.Contains(string.Format("<sessionVal>{0}</sessionVal>", textToSet1)));
            TestHelpers_v2_0.DoRequest(url3, cookieContainer);
            result = TestHelpers_v2_0.DoRequest(url4, cookieContainer);
            // The expected text after abandon is an empty string
            StringAssert.Contains(result, string.Format("<sessionVal>{0}</sessionVal>", ""));
        }     
    }
}
