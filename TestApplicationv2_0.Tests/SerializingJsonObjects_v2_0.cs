using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace TestApplication2_0.Tests
{
    [TestClass]
    public class SerializingJsonObjects_v2_0
    {
        [TestMethod]
        public void JSonObectWithList()
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest request1 =
                (HttpWebRequest)WebRequest.Create(
                TestHelpers_v2_0.BASE_URL +
                TestHelpers_v2_0.SET_SESSION_VAL_JSON_SERIALIZELIST),
                request2 = (HttpWebRequest)WebRequest.Create(
                TestHelpers_v2_0.BASE_URL +
                TestHelpers_v2_0.PRINT_SESSION_VAL_JSON_SERIALIZELIST);

            TestHelpers_v2_0.DoRequest(request1, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(request2, cookieContainer);
            string expectedResultPart = @"<fieldset>
        <legend>PersonPetsList</legend>

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
        <div class=""display-field"">
            Barcelona
        </div>        
        
        <div class=""display-field"">
            Dog
        </div>
        
        <div class=""display-field"">
            Cat
        </div>
        
        <div class=""display-field"">
            Shark
        </div>
        
    </fieldset>
";
            StringAssert.Contains(result, expectedResultPart);
        }

        [TestMethod]
        public void JSonObject()
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest request1 =
                (HttpWebRequest)WebRequest.Create(
                TestHelpers_v2_0.BASE_URL +
                TestHelpers_v2_0.SET_SESSION_VAL_JSON_SERIALIZEPERSON),
                request2 = (HttpWebRequest)WebRequest.Create(
                TestHelpers_v2_0.BASE_URL +
                TestHelpers_v2_0.PRINT_SESSION_VAL_JSON_SERIALIZEPERSON);

            TestHelpers_v2_0.DoRequest(request1, cookieContainer);
            string result = TestHelpers_v2_0.DoRequest(request2, cookieContainer);
            string expectedResultPart = @"<legend>Person</legend>
    
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
        </div>";
            StringAssert.Contains(result, expectedResultPart);
        }
    }
}
