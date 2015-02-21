using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplication.Models;

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

        public ActionResult SetSessionValInt(int newSesVal = 0)
        {
            Session["value"] = newSesVal;
            return View("~/Views/Default/SetSessionVal.aspx");
        }

        public ActionResult SetSessionValBool(bool newSesVal = false)
        {
            Session["value"] = newSesVal;
            return View("~/Views/Default/SetSessionVal.aspx");
        }

        public ActionResult SetSessionValDecimal()
        {
            decimal newSesVal = 3.1416m;
            Session["value"] = newSesVal;
            return View("~/Views/Default/SetSessionVal.aspx");
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

        public ActionResult SerializePerson(
            string name = "Marc",
            string surname = "Cortada",
            string city = "Barcelona")
        {
            Person p = new Person()
            {
                Name = name,
                Surname = surname,
                City = city
            };

            Session["person"] = p;

            return View();
        }

        public ActionResult GetSerializedPerson()
        {
            Person p = new Person();
            if (Session["person"] != null)
            {
                Newtonsoft.Json.Linq.JObject jsonObj = Session["person"] as Newtonsoft.Json.Linq.JObject;
                if (jsonObj != null)
                    p = jsonObj.ToObject<Person>();
            }
            return View(p);
        }

        public ActionResult SerializePersonWithLists(
            string name = "Marc",
            string surname = "Cortada",
            string city = "Barcelona")
        {
            PersonPetsList p = new PersonPetsList()
            {
                Name = name,
                Surname = surname,
                City = city,
                PetsList = new List<string>() { "Dog", "Cat", "Shark" }
            };

            Session["personWithPetsList"] = p;

            return View();
        }

        public ActionResult GetSerializedPersonWithPets()
        {
            PersonPetsList p = new PersonPetsList();
            if (Session["personWithPetsList"] != null)
            {
                Newtonsoft.Json.Linq.JObject jsonObj = Session["personWithPetsList"] as Newtonsoft.Json.Linq.JObject;
                if (jsonObj != null)
                    p = jsonObj.ToObject<PersonPetsList>();
            }
            return View(p);
        }
    }
}
