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
        private const string KEY_NAME3 = "value3";
        public const string VIEW_DATA_VAL = "sessionVal";

        public ActionResult Index()
        {
            Session.Mongo<string>(KEY_NAME, "Hi");
            return View();
        }

        public ActionResult GetAndSetSameRequest()
        {
            Person personSet = new Person() { Name = "Marc", Surname = "Cortada", City = "Barcelona" };
            Session.Mongo<Person>(KEY_NAME, personSet);

            PersonPetsList personPetsSet = new PersonPetsList()
            {
                Name = "Marc2",
                Surname = "Cortada2",
                PetsList = new List<string>() { "cat", "dog" }
            };
            Session.Mongo<PersonPetsList>(KEY_NAME3, personPetsSet);
            
            int setValInt = 3;
            Session.Mongo<int>(KEY_NAME2, setValInt);

            Person personGet = Session.Mongo<Person>(KEY_NAME);
            PersonPetsList personPetsListGet = Session.Mongo<PersonPetsList>(KEY_NAME3);
            int getValInt = Session.Mongo<int>(KEY_NAME2);

            if ((setValInt == getValInt) &&
                (personSet.Name == personGet.Name) &&
                (personSet.Surname == personGet.Surname) &&
                (personSet.City == personGet.City) &&
                (personPetsListGet.Name == personPetsSet.Name) &&
                (personPetsListGet.Surname == personPetsSet.Surname) &&
                (personPetsListGet.PetsList[0] == personPetsSet.PetsList[0]) &&
                (personPetsListGet.PetsList[1] == personPetsSet.PetsList[1]))
                ViewBag.allOk = "True";
            else
                ViewBag.allOk = "False";

            return View();
        }

        public ActionResult PrintSessionSerializedPerson()
        {
            Person p = Session.Mongo<Person>(KEY_NAME);
            return View("~/Views/Default/GetSerializedPerson.aspx", p);
        }

        public ActionResult PrintSessionSerializedPersonWithlist()
        {
            PersonPetsList p = Session.Mongo<PersonPetsList>(KEY_NAME3);
            return View("~/Views/Default/GetSerializedPersonWithPets.aspx", p);
        }

        public ActionResult SetSessionValString(string newSesVal = "")
        {
            Session.Mongo<string>(KEY_NAME, newSesVal);

            return View("~/Views/Default/SetSessionVal.aspx");
        }

        public ActionResult PrintSessionValString()
        {
            string val = Session.Mongo<string>(KEY_NAME);

            ViewBag.sessionVal = val;
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult SetSessionValDouble()
        {
            double newSesVal = 3.1416F;
            Session.Mongo<double>(KEY_NAME, newSesVal);

            return View("~/Views/Default/SetSessionVal.aspx");
        }

        public ActionResult PrintSessionValDouble()
        {
            double dobVal = Session.Mongo<double>(KEY_NAME);

            ViewBag.sessionVal = string.Format("{0:0.0000}", dobVal);
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult SetSessionValInt(int newSesVal = 0)
        {
            Session.Mongo<int>(KEY_NAME, newSesVal);

            return View("~/Views/Default/SetSessionVal.aspx");
        }

        public ActionResult PrintSessionValInt()
        {
            int intVal = Session.Mongo<int>(KEY_NAME);

            ViewBag.sessionVal = intVal;
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult SetSessionValBool(bool newSesVal = false)
        {
            Session.Mongo<bool>(KEY_NAME, newSesVal);

            return View("~/Views/Default/SetSessionVal.aspx");
        }

        public ActionResult PrintSessionValBool()
        {
            bool boolVal = Session.Mongo<bool>(KEY_NAME);

            ViewBag.sessionVal = boolVal;
            return View("~/Views/Default/PrintSessionVal.aspx");
        }
    }
}
