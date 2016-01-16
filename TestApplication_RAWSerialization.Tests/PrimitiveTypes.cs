using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace TestApplication_RAWSerialization.Tests
{
    [TestClass]
    public class PrimitiveTypes
    {
        private string _controller = "Home";

        [TestMethod]
        public void TestInt()
        {
            CookieContainer cookie = new CookieContainer();
            string url1 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                _controller, "SetIntVal");
            string url2 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                _controller, "GetIntVal");
            
            string result = TestApplication_RAW_Helpers.DoRequest(url1, cookie);
            string result2 = TestApplication_RAW_Helpers.DoRequest(url2, cookie);
            StringAssert.Contains(result, "<sessionVal>3</sessionVal>");
            StringAssert.Contains(result2, "<sessionVal>3</sessionVal>");
        }

        [TestMethod]
        public void TestString()
        {
            CookieContainer cookie = new CookieContainer();
            string url1 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                _controller, "SetStringVal");
            string url2 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                _controller, "GetStringVal");

            string result1 = TestApplication_RAW_Helpers.DoRequest(url1, cookie);
            string result2 = TestApplication_RAW_Helpers.DoRequest(url2, cookie);
            StringAssert.Contains(result1, "<sessionVal>Barcelona</sessionVal>");
            StringAssert.Contains(result2, "<sessionVal>Barcelona</sessionVal>");
        }

        [TestMethod]
        public void TestObjects()
        {
            CookieContainer cookie = new CookieContainer();
            string url1 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                _controller, "GetAndSetSameRequestObjects");
            string url2 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                _controller, "GetObjects");

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
                _controller, "GetAndSetSameRequestObjects");
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
                _controller, "GetAndSetSameRequestObjects");
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
                _controller, "GetAndSetSameRequestObjects");
            string url2 = string.Format(TestApplication_RAW_Helpers.BASE_URL_2,
                "Home", "Index");
            string url3 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                _controller, "GetObjects");
            string url4 = string.Format(TestApplication_RAW_Helpers.BASE_URL,
                "GetWithoutType", "Index");

            string result1 = TestApplication_RAW_Helpers.DoRequest(url1, cookie);
            string result2 = TestApplication_RAW_Helpers.DoRequest(url2, cookie);
            string result3 = TestApplication_RAW_Helpers.DoRequest(url3, cookie);
            string result4 = TestApplication_RAW_Helpers.DoRequest(url4, cookie);

            StringAssert.Contains(result1, "<sessionVal>True</sessionVal>");
            StringAssert.Contains(result2, "<sessionVal>True</sessionVal>");
            StringAssert.Contains(result3, "<sessionVal>True</sessionVal>");
            StringAssert.Contains(result4, "<sessionVal>True</sessionVal>");
        }
    }
}
