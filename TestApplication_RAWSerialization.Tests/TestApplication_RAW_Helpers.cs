using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication_RAWSerialization.Tests
{
    internal static class TestApplication_RAW_Helpers
    {
        public static string BASE_URL = "http://localhost/TestApplication_RAWSerialization/{0}/{1}";
        public static string BASE_URL_2 = "http://localhost/TestApplication_RAWSerializationWithoutTypeReference/{0}/{1}";

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
