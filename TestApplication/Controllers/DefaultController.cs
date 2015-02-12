using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestApplication.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/

        public ActionResult Index()
        {
            Session["value"] = "Hi";
            return View();
        }

        public ActionResult PrintSessionVal()
        {
            string val = (Session["value"] == null) ? "" : Session["value"].ToString();
            ViewData["sessionVal"] = val;
            return View();
        }

        public ActionResult SetSessionVal(string newSesVal = "")
        {
            Session["value"] = newSesVal;
            return View();
        }

        public ActionResult SessionAbandon()
        {
            Session.Abandon();
            return View();
        }

        public ActionResult SetSessionValWaiting(string newSesVal = "")
        {
            Session["value"] = newSesVal;
            System.Threading.Thread.CurrentThread.Join(10000);
            Session["value"] = newSesVal + "2";
            return View();
        }
    }
}
