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
            string url =
                                    TestHelpers_v2_0.DEFAULT_WITH_BSON +
                                    TestHelpers_v2_0.SET_BSON_VAL,
            url2 =
                    TestHelpers_v2_0.DEFAULT_WITH_BSON +
                    TestHelpers_v2_0.GET_BSON_VAL;
            TestHelpers_v2_0.DoRequest(url, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(url2, cookieContainer);
            StringAssert.Contains(result, @"<result>
            Name: Marc
            Surname: Cortada
            City: Barcelona
        </result>");
        }
    }
}
