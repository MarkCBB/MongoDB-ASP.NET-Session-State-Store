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
        private const string KEY_NAME = "value";
        private const string KEY_NAME2 = "value2";
        private const string KEY_NAME3 = "value3";
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

            Person personGetted = (Person)Session[KEY_NAME];
            PersonPetsList personPetsListGetted = (PersonPetsList)Session[KEY_NAME3];
            int gettedValInt = (int)Session[KEY_NAME2];

            if ((settedValInt == gettedValInt) &&
                (personSetted.Name == personGetted.Name) &&
                (personSetted.Surname == personGetted.Surname) &&
                (personSetted.City == personGetted.City) &&
                (personPetsListGetted.Name == personPetsSetted.Name) &&
                (personPetsListGetted.Surname == personPetsSetted.Surname) &&
                (personPetsListGetted.PetsList[0] == personPetsSetted.PetsList[0]) &&
                (personPetsListGetted.PetsList[1] == personPetsSetted.PetsList[1]))
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

            Person personGetted = (Person)Session[KEY_NAME];
            PersonPetsList personPetsListGetted = (PersonPetsList)Session[KEY_NAME3];
            int gettedValInt = (int)Session[KEY_NAME2];

            if ((settedValInt == gettedValInt) &&
                (personSetted.Name == personGetted.Name) &&
                (personSetted.Surname == personGetted.Surname) &&
                (personSetted.City == personGetted.City) &&
                (personPetsListGetted.Name == personPetsSetted.Name) &&
                (personPetsListGetted.Surname == personPetsSetted.Surname) &&
                (personPetsListGetted.PetsList[0] == personPetsSetted.PetsList[0]) &&
                (personPetsListGetted.PetsList[1] == personPetsSetted.PetsList[1]))
                ViewBag.sessionVal = "True";
            else
                ViewBag.sessionVal = "False";

            return View("~/Views/Home/Index.aspx");
        }
    }
}
