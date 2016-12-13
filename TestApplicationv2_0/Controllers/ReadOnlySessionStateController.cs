using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace TestApplicationv2_0.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class ReadOnlySessionStateController : Controller
    {
        public ActionResult ReadLongRunningValueProcess()
        {
            int retVal = Session.Mongo<int>(
                DefaultWithHelpersController.LONG_RUNNING_VALUE);
            ViewBag.sessionVal = retVal;
            return View("~/Views/Default/PrintSessionVal.aspx");
        }
    }
}
