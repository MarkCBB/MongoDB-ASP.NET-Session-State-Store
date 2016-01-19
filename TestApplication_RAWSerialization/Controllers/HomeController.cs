using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplication_RAWSerialization.Models;

namespace TestApplication_RAWSerialization.Controllers
{
    public class HomeController : Controller
    {
        public const string KEY_NAME = "value";
        public const string KEY_NAME2 = "value2";
        public const string KEY_NAME3 = "value3";
        public const string VIEW_DATA_VAL = "sessionVal";

        public ActionResult SetIntVal(int val = 3)
        {
            Session["SetInteger"] = val;
            int valForGet = (int)Session["SetInteger"];
            ViewBag.sessionVal = valForGet;
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult GetIntVal()
        {
            int valForGet = (int)Session["SetInteger"];
            ViewBag.sessionVal = valForGet;
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult SetDoubleVal()
        {
            double val = 3.1416;
            Session["SetInteger"] = val;
            double valForGet = (double)Session["SetInteger"];
            ViewBag.sessionVal = valForGet;
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult GetDoubleVal()
        {
            double valForGet = (double)Session["SetInteger"];
            ViewBag.sessionVal = valForGet;
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult SetIntNullableVal()
        {
            int? valForSet = 3;
            Session["SetNullableInteger"] = valForSet;
            int? valForGet = (int?)Session["SetNullableInteger"];
            ViewBag.sessionVal = valForGet.ToString();
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult GetIntNullableVal()
        {
            int? valForGet = (int?)Session["SetNullableInteger"];
            ViewBag.sessionVal = valForGet;
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult SetIntNullVal()
        {
            int? val = null;
            Session["SetInteger"] = val;
            int? valForGet = Session["SetIntegerNull"] as int?;
            ViewBag.sessionVal = (valForGet == null) ? "OK" : "KO";
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult GetIntNullVal()
        {
            int? valForGet = (int?)Session["SetIntegerNull"];
            ViewBag.sessionVal = (valForGet == null) ? "OK" : "KO";
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult TestStaticString()
        {
            ViewBag.sessionVal = "OK";
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult SetStringVal(string val = "Barcelona")
        {
            Session["SetStringVal"] = val;
            string valForGet = (string)Session["SetStringVal"];
            ViewBag.sessionVal = valForGet;
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult GetStringVal()
        {
            string valForGet = (string)Session["SetStringVal"];
            ViewBag.sessionVal = valForGet;
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult GetAndSetSameRequestObjects()
        {
            Person personSetted = new Person() { Name = "Marc", Surname = "Cortada", City = "Barcelona" };
            Session[KEY_NAME] = personSetted;

            PersonPetsList personPetsSetted = new PersonPetsList()
            {
                Name = "Marc2",
                Surname = "Cortada2",
                PetsList = new List<string>() { "cat", "dog" }
            };
            Session[KEY_NAME3] = personPetsSetted;

            int settedValInt = 3;
            Session[KEY_NAME2] = settedValInt;

            Person personGet = (Person)Session[KEY_NAME];
            PersonPetsList personPetsListGet = (PersonPetsList)Session[KEY_NAME3];
            int getValInt = (int)Session[KEY_NAME2];

            if ((settedValInt == getValInt) &&
                (personSetted.Name == personGet.Name) &&
                (personSetted.Surname == personGet.Surname) &&
                (personSetted.City == personGet.City) &&
                (personPetsListGet.Name == personPetsSetted.Name) &&
                (personPetsListGet.Surname == personPetsSetted.Surname) &&
                (personPetsListGet.PetsList[0] == personPetsSetted.PetsList[0]) &&
                (personPetsListGet.PetsList[1] == personPetsSetted.PetsList[1]))
                ViewBag.sessionVal = "True";
            else
                ViewBag.sessionVal = "False";

            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult GetObjects()
        {
            Person personSetted = new Person() { Name = "Marc", Surname = "Cortada", City = "Barcelona" };

            PersonPetsList personPetsSetted = new PersonPetsList()
            {
                Name = "Marc2",
                Surname = "Cortada2",
                PetsList = new List<string>() { "cat", "dog" }
            };

            int settedValInt = 3;

            Person personGet = (Person)Session[KEY_NAME];
            PersonPetsList personPetsListGet = (PersonPetsList)Session[KEY_NAME3];
            int getValInt = (int)Session[KEY_NAME2];

            if ((settedValInt == getValInt) &&
                (personSetted.Name == personGet.Name) &&
                (personSetted.Surname == personGet.Surname) &&
                (personSetted.City == personGet.City) &&
                (personPetsListGet.Name == personPetsSetted.Name) &&
                (personPetsListGet.Surname == personPetsSetted.Surname) &&
                (personPetsListGet.PetsList[0] == personPetsSetted.PetsList[0]) &&
                (personPetsListGet.PetsList[1] == personPetsSetted.PetsList[1]))
                ViewBag.sessionVal = "True";
            else
                ViewBag.sessionVal = "False";

            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult SetNullObject()
        {
            Person personSet = null;
            Session["PersonNull"] = personSet;
            Person personGet = Session["PersonNull"] as Person;
            ViewBag.sessionVal = (personGet == null) ? "OK" : "KO";
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult GetNullObject()
        {
            Person personGet = Session["PersonNull"] as Person;
            ViewBag.sessionVal = (personGet == null) ? "OK" : "KO";
            return View("~/Views/Home/Index.aspx");
        }
    }
}
