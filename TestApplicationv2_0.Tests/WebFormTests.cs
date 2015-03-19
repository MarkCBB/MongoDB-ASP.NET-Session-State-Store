using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using TestApplication2_0.Tests;

namespace TestApplicationv2_0.Tests
{
    [TestClass]
    public class WebFormTests
    {
        [TestMethod]
        public void GetAndSetValues()
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(TestHelpers_v2_0.BASE_URL_FORMS +
                TestHelpers_v2_0.SET_VALUE_WEB_FORM),
                request2 =
                (HttpWebRequest)WebRequest.Create(TestHelpers_v2_0.BASE_URL_FORMS +
                TestHelpers_v2_0.GET_VALUE_WEB_FORM);

            string result = TestHelpers_v2_0.DoRequest(request, cookieContainer);
            StringAssert.Contains(result, "<result>OK</result>");

            string result2 = TestHelpers_v2_0.DoRequest(request2, cookieContainer);
            StringAssert.Contains(result2, @"<result1>314</result1>");
            StringAssert.Contains(result2, @"<result2>3,14</result2>");        
            StringAssert.Contains(result2, @"<result3>Name: Marc, surname: Cortada</result3>");
            StringAssert.Contains(result2, @"<result4>Name: Marc2, surname: Cortada2, pet 1 cat, pet 2 dog</result4>");
        }
    }
}
