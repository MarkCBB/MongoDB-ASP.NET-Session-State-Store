using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using TestApplication2_0.Tests;

namespace TestApplicationv2_0.Tests
{
    [TestClass]
    public class BSONDocumentTest
    {
        [TestMethod]
        public void PritnBSONPerson()
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                                    TestHelpers_v2_0.DEFAULT_WITH_BSON +
                                    TestHelpers_v2_0.SET_BSON_VAL),
                            request2 = (HttpWebRequest)WebRequest.Create(
                                    TestHelpers_v2_0.DEFAULT_WITH_BSON +
                                    TestHelpers_v2_0.GET_BSON_VAL);
            TestHelpers_v2_0.DoRequest(request, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(request2, cookieContainer);
            StringAssert.Contains(result, @"<result>
            Name: Marc
            Surname: Cortada
            City: Barcelona
        </result>");
        }
    }
}
