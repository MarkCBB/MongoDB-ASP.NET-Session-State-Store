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
    public class SimplyGetAndSet
    {
        /// <summary>
        /// This test case does a request without value assignation, so a blank string is expected
        /// </summary>
        [TestMethod]
        public void InitBlankValue()
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.PRINT_SESION_ACTION);
            string result = TestHelpers.DoRequest(request, cookieContainer);
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
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.SET_SESSION_ACTION + textToSet),
                request2 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.PRINT_SESION_ACTION);
            TestHelpers.DoRequest(request1, cookieContainer);
            string result = TestHelpers.DoRequest(request2, cookieContainer);
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
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.SET_SESSION_ACTION + textToSet1),
                request2 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.SET_SESSION_ACTION + textToSet2),
                request3 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.PRINT_SESION_ACTION);
            TestHelpers.DoRequest(request1, cookieContainer);
            TestHelpers.DoRequest(request2, cookieContainer);
            string result = TestHelpers.DoRequest(request3, cookieContainer);
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
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.SET_SESSION_VAL_INT + intToSet),
                request2 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.PRINT_SESION_ACTION);
            TestHelpers.DoRequest(request1, cookieContainer);
            string result = TestHelpers.DoRequest(request2, cookieContainer);
            StringAssert.Contains(result, string.Format("<sessionVal>{0}</sessionVal>", intToSet));
        }

        /// <summary>
        /// This test case assigns a value to a SessionState 
        /// </summary>
        [TestMethod]
        public void SingleSetValueDecimal()
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.SET_SESSION_VAL_DECIMAL),
                request2 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.PRINT_SESION_ACTION);
            TestHelpers.DoRequest(request1, cookieContainer);
            string result = TestHelpers.DoRequest(request2, cookieContainer);
            StringAssert.Contains(result, string.Format("<sessionVal>{0}</sessionVal>", "3,1416"));
        }

        /// <summary>
        /// This test case assigns a value to a SessionState 
        /// </summary>
        [TestMethod]
        public void SingleSetValueBool()
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.SET_SESSION_VAL_BOOL + true),
                request2 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.PRINT_SESION_ACTION),
                request3 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.SET_SESSION_VAL_BOOL + false),
                request4 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.PRINT_SESION_ACTION);           
            
            //Trying setting with true
            TestHelpers.DoRequest(request1, cookieContainer);
            string result = TestHelpers.DoRequest(request2, cookieContainer);
            StringAssert.Contains(result, string.Format("<sessionVal>{0}</sessionVal>", "True"));

            //Trying setting with false
            TestHelpers.DoRequest(request3, cookieContainer);
            result = TestHelpers.DoRequest(request4, cookieContainer);
            StringAssert.Contains(result, string.Format("<sessionVal>{0}</sessionVal>", "False"));
        }

        /// <summary>
        /// This test case assigns a value to a SessionState twice
        /// </summary>
        [TestMethod]
        public void SingleValueTwice()
        {
            CookieContainer cookieContainer = new CookieContainer();
            string textToSet1 = "valueSettedInSession", textToSet2 = "Second valueSetted In Session state";
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.SET_SESSION_ACTION + textToSet1),
                request2 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.PRINT_SESION_ACTION),
                request3 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.SET_SESSION_ACTION + textToSet2),
                request4 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.PRINT_SESION_ACTION);
            TestHelpers.DoRequest(request1, cookieContainer);
            string result = TestHelpers.DoRequest(request2, cookieContainer);
            StringAssert.Contains(result, string.Format("<sessionVal>{0}</sessionVal>", textToSet1));
            TestHelpers.DoRequest(request3, cookieContainer);
            result = TestHelpers.DoRequest(request4, cookieContainer);
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
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.SET_SESSION_ACTION + textToSet1),
                request2 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.PRINT_SESION_ACTION),
                request3 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.SESSION_ABANDON_ACTION),
                request4 = (HttpWebRequest)WebRequest.Create(TestHelpers.BASE_URL + TestHelpers.PRINT_SESION_ACTION);
            TestHelpers.DoRequest(request1, cookieContainer);
            string result = TestHelpers.DoRequest(request2, cookieContainer);
            Assert.IsTrue(result.Contains(string.Format("<sessionVal>{0}</sessionVal>", textToSet1)));
            TestHelpers.DoRequest(request3, cookieContainer);
            result = TestHelpers.DoRequest(request4, cookieContainer);
            // The expected text after abandon is an empty string
            StringAssert.Contains(result, string.Format("<sessionVal>{0}</sessionVal>", ""));
        }     
    }
}
