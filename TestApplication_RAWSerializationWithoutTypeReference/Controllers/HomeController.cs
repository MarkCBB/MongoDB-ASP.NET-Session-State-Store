using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestApplication_RAWSerializationWithoutTypeReference.Controllers
{
    public class HomeController : Controller
    {
        public const string KEY_NAME = "value";
        public const string KEY_NAME2 = "value2";
        public const string KEY_NAME3 = "value3";
        public const string VIEW_DATA_VAL = "sessionVal";
        //
        // GET: /Home/

        public ActionResult Index()
        {
            dynamic personGet = Session[HomeController.KEY_NAME];
            dynamic personPetsListGet = Session[HomeController.KEY_NAME3];

            if ((personGet is MongoSessionStateStore.Serialization.UnSerializedItem) &&
                (personPetsListGet is MongoSessionStateStore.Serialization.UnSerializedItem))
                ViewBag.sessionVal = "True";
            else
                ViewBag.sessionVal = "False";

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
    }
}
