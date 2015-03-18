using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplicationv2_0.Models;

namespace TestApplicationv2_0.Controllers
{
    public class DefaultWithHelpersController : Controller
    {
        //
        // GET: /DefaultWithHelpers/

        private const string KEY_NAME = "value";
        private const string KEY_NAME2 = "value2";
        public const string VIEW_DATA_VAL = "sessionVal";

        public ActionResult Index()
        {
            Session.Mongo<string>(KEY_NAME, "Hi");
            return View();
        }

        public ActionResult GetAndSetSameRequest()
        {
            Person personSetted = new Person() { Name = "Marc", Surname = "Cortada", City = "Barcelona" };
            Session.Mongo<Person>(KEY_NAME, personSetted);
            
            int settedValInt = 3;
            Session.Mongo<int>(KEY_NAME2, settedValInt);

            Person personGetted = Session.Mongo<Person>(KEY_NAME);
            int gettedValInt = Session.Mongo<int>(KEY_NAME2);

            if ((settedValInt == gettedValInt) && (personSetted == personGetted))
                ViewBag.allOk = "True";
            else
                ViewBag.allOk = "False";

            return View();
        }

        public ActionResult PrintSessionValString()
        {
            string val = Session.Mongo<string>(KEY_NAME);

            ViewBag.sessionVal = val;
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult PrintSessionValDouble()
        {
            double dobVal = Session.Mongo<double>(KEY_NAME);

            ViewBag.sessionVal = dobVal.ToString("G");
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult SetSessionValInt(int newSesVal = 0)
        {
            Session.Mongo<int>(KEY_NAME, newSesVal);

            return View("~/Views/Default/SetSessionVal.aspx");
        }

        public ActionResult SetSessionValBool(bool newSesVal = false)
        {
            Session.Mongo<bool>(KEY_NAME, newSesVal);

            return View("~/Views/Default/SetSessionVal.aspx");
        }

        public ActionResult SetSessionValDouble()
        {
            double newSesVal = 3.1416F;
            Session.Mongo<double>(KEY_NAME, newSesVal);

            return View("~/Views/Default/SetSessionVal.aspx");
        }

        public ActionResult SetSessionVal(string newSesVal = "")
        {
            Session.Mongo<string>(KEY_NAME, newSesVal);

            return View();
        }
    }
}
