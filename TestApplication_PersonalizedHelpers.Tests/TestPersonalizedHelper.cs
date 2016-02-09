using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace TestApplication_PersonalizedHelpers.Tests
{
    [TestClass]
    public class TestPersonalizedHelper
    {
        //This test case is quite huge, but the order in the requests is important.
        [TestMethod]
        public void TestMvcPersonalizedHelper()
        {
            CookieContainer cookie = new CookieContainer();
            string url1 = string.Format(TestApplication_PersonalizedHelpers_Helpers.BASE_URL_MVC,
                TestApplication_PersonalizedHelpers_Helpers.CONTROLLER, "SetTestPersonalizedHelper");
            string url2 = string.Format(TestApplication_PersonalizedHelpers_Helpers.BASE_URL_MVC,
                TestApplication_PersonalizedHelpers_Helpers.CONTROLLER, "GetTestPersonalizedHelper");
            string url3 = string.Format(TestApplication_PersonalizedHelpers_Helpers.BASE_URL_MVC,
                TestApplication_PersonalizedHelpers_Helpers.CONTROLLER, "ChangeHelperToFullPersonalized");
            string url4 = string.Format(TestApplication_PersonalizedHelpers_Helpers.BASE_URL_FORMS,
                TestApplication_PersonalizedHelpers_Helpers.WEB_FORM1);
            string url5 = string.Format(TestApplication_PersonalizedHelpers_Helpers.BASE_URL_FORMS,
                TestApplication_PersonalizedHelpers_Helpers.WEB_FORM2);
            string url6 = string.Format(TestApplication_PersonalizedHelpers_Helpers.BASE_URL_MVC,
                TestApplication_PersonalizedHelpers_Helpers.CONTROLLER, "ChangeHelperToPartialPersonalized");

            string result1 = TestApplication_PersonalizedHelpers_Helpers.DoRequest(url1, cookie);
            string result2 = TestApplication_PersonalizedHelpers_Helpers.DoRequest(url2, cookie);
            string result3 = TestApplication_PersonalizedHelpers_Helpers.DoRequest(url3, cookie);
            string result4 = TestApplication_PersonalizedHelpers_Helpers.DoRequest(url4, cookie);
            string result5 = TestApplication_PersonalizedHelpers_Helpers.DoRequest(url5, cookie);
            string result6 = TestApplication_PersonalizedHelpers_Helpers.DoRequest(url6, cookie);

            StringAssert.Contains(result1,
                "<sessionVal>This is a sample class of a partial personalized helper</sessionVal>");
            StringAssert.Contains(result2,
                "<sessionVal>This is a sample class of a partial personalized helper</sessionVal>");
            StringAssert.Contains(result3, "<sessionVal>OK</sessionVal>");
            StringAssert.Contains(result4,
                "<sessionVal>This is a sample class of a full personalized helper</sessionVal>");
            StringAssert.Contains(result5,
                "<sessionVal>This is a sample class of a full personalized helper</sessionVal>");
            StringAssert.Contains(result6, "<sessionVal>OK</sessionVal>");
        }
    }
}
