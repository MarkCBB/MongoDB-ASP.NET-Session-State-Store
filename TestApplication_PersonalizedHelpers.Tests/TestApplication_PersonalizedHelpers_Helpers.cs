using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication_PersonalizedHelpers.Tests
{
    class TestApplication_PersonalizedHelpers_Helpers
    {
        public static string BASE_URL_MVC = "http://localhost/TestApplication_PersonalizedHelpers/{0}/{1}";
        public static string BASE_URL_FORMS = "http://localhost/TestApplication_PersonalizedHelpers/{0}";
        public static string CONTROLLER = "home";
        public static string WEB_FORM1 = "WebFormGetData.aspx";
        public static string WEB_FORM2 = "WebFormSetData.aspx";

        public static string DoRequest(
            string url,
            CookieContainer cookieContainer)
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cookieContainer;
            var stream = request.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        } 
    }
}
