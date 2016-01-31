using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestApplication_PersonalizedHelpers.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.sessionVal = "OK";
            return View();
        }

        public ActionResult SetTestPersonalizedHelper()
        {
            Session.Mongo<string>("PersonalizedHelper", "Test string");
            ViewBag.sessionVal = Session.Mongo<string>("PersonalizedHelper");
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult GetTestPersonalizedHelper()
        {
            ViewBag.sessionVal = Session.Mongo<string>("PersonalizedHelper");
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult ChangeHelperToFullPersonalized()
        {
            //Do NOT set the helper class here. This is only for test purposes.
            //Is recommended to do it in an App_Start class and call the method in Application_Start event.
            System.Web.Mvc.MongoSessionUserHelpersMvc.SetHelper(new SessionHelperPersonalized());
            ViewBag.sessionVal = "OK";
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult ChangeHelperToPartialPersonalized()
        {
            //Do NOT set the helper class here. This is only for test purposes.
            //Is recommended to do it in an App_Start class and call the method in Application_Start event.
            System.Web.Mvc.MongoSessionUserHelpersMvc.SetHelper(new SessionHelperPersonalizedPartial());
            ViewBag.sessionVal = "OK";
            return View("~/Views/Home/Index.aspx");
        }
    }
}
