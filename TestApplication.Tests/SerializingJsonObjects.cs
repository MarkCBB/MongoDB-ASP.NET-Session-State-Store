using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace TestApplication.Tests
{
    [TestClass]
    public class SerializingJsonObjects
    {
        [TestMethod]
        public void JSonObectWithList()
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest request1 =
                (HttpWebRequest)WebRequest.Create(
                TestHelpers.BASE_URL +
                TestHelpers.SET_SESSION_VAL_JSON_SERIALIZELIST),
                request2 = (HttpWebRequest)WebRequest.Create(
                TestHelpers.BASE_URL +
                TestHelpers.PRINT_SESSION_VAL_JSON_SERIALIZELIST);

            TestHelpers.DoRequest(request1, cookieContainer);
            string result = TestHelpers.DoRequest(request2, cookieContainer);
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
                TestHelpers.BASE_URL +
                TestHelpers.SET_SESSION_VAL_JSON_SERIALIZEPERSON),
                request2 = (HttpWebRequest)WebRequest.Create(
                TestHelpers.BASE_URL +
                TestHelpers.PRINT_SESSION_VAL_JSON_SERIALIZEPERSON);

            TestHelpers.DoRequest(request1, cookieContainer);
            string result = TestHelpers.DoRequest(request2, cookieContainer);
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
