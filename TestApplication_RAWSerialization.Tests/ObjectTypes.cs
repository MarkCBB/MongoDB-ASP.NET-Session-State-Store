using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace TestApplication_RAWSerialization.Tests
{
    [TestClass]
    public class ObjectTypes
    {
        [TestMethod]
        public void TestObjects()
        {
            CookieContainer cookie = new CookieContainer();
            string url1 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                TestApplication_RAW_Helpers.CONTROLLER, "GetAndSetSameRequestObjects");
            string url2 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                TestApplication_RAW_Helpers.CONTROLLER, "GetObjects");

            string result1 = TestApplication_RAW_Helpers.DoRequest(url1, cookie);
            string result2 = TestApplication_RAW_Helpers.DoRequest(url2, cookie);
            StringAssert.Contains(result1, "<sessionVal>True</sessionVal>");
            StringAssert.Contains(result2, "<sessionVal>True</sessionVal>");
        }

        [TestMethod]
        public void TestObjectsWithOutType()
        {
            CookieContainer cookie = new CookieContainer();
            string url1 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                TestApplication_RAW_Helpers.CONTROLLER, "GetAndSetSameRequestObjects");
            string url2 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                "GetWithoutType", "Index");

            string result1 = TestApplication_RAW_Helpers.DoRequest(url1, cookie);
            string result2 = TestApplication_RAW_Helpers.DoRequest(url2, cookie);
            StringAssert.Contains(result1, "<sessionVal>True</sessionVal>");
            StringAssert.Contains(result2, "<sessionVal>True</sessionVal>");
        }

        [TestMethod]
        public void TestObjectsWithOutTypeExternalProject()
        {
            CookieContainer cookie = new CookieContainer();
            string url1 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                TestApplication_RAW_Helpers.CONTROLLER, "GetAndSetSameRequestObjects");
            string url2 = string.Format(TestApplication_RAW_Helpers.BASE_URL_2,
                "Home", "Index");

            string result1 = TestApplication_RAW_Helpers.DoRequest(url1, cookie);
            string result2 = TestApplication_RAW_Helpers.DoRequest(url2, cookie);
            StringAssert.Contains(result1, "<sessionVal>True</sessionVal>");
            StringAssert.Contains(result2, "<sessionVal>True</sessionVal>");
        }

        [TestMethod]
        public void TestObjectsWithOutTypeExternalProjectAndRequestToFirstProject()
        {
            CookieContainer cookie = new CookieContainer();
            string url1 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                TestApplication_RAW_Helpers.CONTROLLER, "GetAndSetSameRequestObjects");
            string url2 = string.Format(TestApplication_RAW_Helpers.BASE_URL_2,
                TestApplication_RAW_Helpers.CONTROLLER, "Index");
            string url3 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                TestApplication_RAW_Helpers.CONTROLLER, "GetObjects");
            string url4 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                TestApplication_RAW_Helpers.CONTROLLER2, "Index");

            string result1 = TestApplication_RAW_Helpers.DoRequest(url1, cookie);
            string result2 = TestApplication_RAW_Helpers.DoRequest(url2, cookie);
            string result3 = TestApplication_RAW_Helpers.DoRequest(url3, cookie);
            string result4 = TestApplication_RAW_Helpers.DoRequest(url4, cookie);

            StringAssert.Contains(result1, "<sessionVal>True</sessionVal>");
            StringAssert.Contains(result2, "<sessionVal>True</sessionVal>");
            StringAssert.Contains(result3, "<sessionVal>True</sessionVal>");
            StringAssert.Contains(result4, "<sessionVal>True</sessionVal>");
        }

        [TestMethod]
        public void TestObjectsNullValues()
        {
            CookieContainer cookie = new CookieContainer();
            string url1 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                TestApplication_RAW_Helpers.CONTROLLER, "SetNullObject");
            string url2 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                TestApplication_RAW_Helpers.CONTROLLER, "GetNullObject");

            string result1 = TestApplication_RAW_Helpers.DoRequest(url1, cookie);
            string result2 = TestApplication_RAW_Helpers.DoRequest(url2, cookie);

            StringAssert.Contains(result1, "<sessionVal>OK</sessionVal>");
            StringAssert.Contains(result2, "<sessionVal>OK</sessionVal>");
        }

        [TestMethod]
        public void TestGetNonExistingKey()
        {
            CookieContainer cookie = new CookieContainer();
            string url1 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                TestApplication_RAW_Helpers.CONTROLLER, "GetNonExistingKey");

            string result1 = TestApplication_RAW_Helpers.DoRequest(url1, cookie);

            StringAssert.Contains(result1, "<sessionVal>OK</sessionVal>");
        }
    }
}
