using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;

namespace TestApplication.Tests
{
    [TestClass]
    public class ElectionsTest_v1_0
    {
        private Object _lockObj = new Object();
        private bool _testOk = true;
        private string _errorMessage = "";
        private int nCall = 0;
        private int nBlock = 0;

        public void SingleSetValueThread()
        {
            try
            {
                CookieContainer cookieContainer = new CookieContainer();
                string textToSet = "valueSettedInSession" + (nBlock * nCall);
                HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(TestHelpers_v1_0.BASE_URL + TestHelpers_v1_0.SET_SESSION_ACTION + textToSet),
                    request2 = (HttpWebRequest)WebRequest.Create(TestHelpers_v1_0.BASE_URL + TestHelpers_v1_0.PRINT_SESION_ACTION);
                TestHelpers_v1_0.DoRequest(request1, cookieContainer);
                string result = TestHelpers_v1_0.DoRequest(request2, cookieContainer);
                lock (_lockObj)
                {
                    if ((_testOk) &&
                        (!result.Contains(string.Format("<sessionVal>{0}</sessionVal>", textToSet))))
                    {
                        _testOk = false;
                        _errorMessage = "Failed. Bad content" + Environment.NewLine + result;
                    }
                }
            }
            catch (Exception e)
            {
                lock (_lockObj)
                {
                    _testOk = false;
                    _errorMessage = "Http Exception" + e.Message;
                }
            }
        }

        public void SendMultipleCallsAsync()
        {
            const int MAX_CALLS = 1000;
            nBlock++;
            List<System.Threading.Thread> tasks = new List<System.Threading.Thread>();
            for (nCall = 0; nCall < MAX_CALLS; nCall++)
            {
                System.Threading.Thread t = new System.Threading.Thread(SingleSetValueThread);
                t.Start();
                tasks.Add(t);
            }
            for (nCall = 0; nCall < MAX_CALLS; nCall++)
            {
                tasks[nCall].Join();
            }
        }

        /// <summary>
        /// During this two minutes sending requests to a web server
        /// you can try to disconnect, switch off and/or kill
        /// the process of the replica set primary server.
        /// During this time requests and responses will be send continuously and
        /// the getted an setted value will be compared. If the elections process
        /// works well the session state will work only with a pause during
        /// the election process but without any interruption.
        /// </summary>
        [TestMethod]
        public void SendRequestsTwoMinutes()
        {
            DateTime final = DateTime.Now.AddMinutes(2);

            while ((_testOk) && (DateTime.Now < final))
                SendMultipleCallsAsync();

            Assert.IsTrue(_testOk, _errorMessage);
        }
    }
}
