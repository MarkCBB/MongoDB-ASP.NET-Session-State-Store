﻿using System;
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
            Person personSet = new Person() { Name = "Marc", Surname = "Cortada", City = "Barcelona" };
            Session[KEY_NAME] = personSet;

            PersonPetsList personPetsSet = new PersonPetsList()
            {
                Name = "Marc2",
                Surname = "Cortada2",
                PetsList = new List<string>() { "cat", "dog" }
            };
            Session[KEY_NAME3] = personPetsSet;

            Person personGet = (Person)Session[KEY_NAME];
            PersonPetsList personPetsListGet = (PersonPetsList)Session[KEY_NAME3];

            if ((personSet.Name == personGet.Name) &&
                (personSet.Surname == personGet.Surname) &&
                (personSet.City == personGet.City) &&
                (personPetsListGet.Name == personPetsSet.Name) &&
                (personPetsListGet.Surname == personPetsSet.Surname) &&
                (personPetsListGet.PetsList[0] == personPetsSet.PetsList[0]) &&
                (personPetsListGet.PetsList[1] == personPetsSet.PetsList[1]))
                ViewBag.sessionVal = "True";
            else
                ViewBag.sessionVal = "False";

            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult GetObjects()
        {
            Person personSet = new Person() { Name = "Marc", Surname = "Cortada", City = "Barcelona" };

            PersonPetsList personPetsSet = new PersonPetsList()
            {
                Name = "Marc2",
                Surname = "Cortada2",
                PetsList = new List<string>() { "cat", "dog" }
            };

            Person personGet = (Person)Session[KEY_NAME];
            PersonPetsList personPetsListGet = (PersonPetsList)Session[KEY_NAME3];

            if ((personSet.Name == personGet.Name) &&
                    (personSet.Surname == personGet.Surname) &&
                    (personSet.City == personGet.City) &&
                    (personPetsListGet.Name == personPetsSet.Name) &&
                    (personPetsListGet.Surname == personPetsSet.Surname) &&
                    (personPetsListGet.PetsList[0] == personPetsSet.PetsList[0]) &&
                    (personPetsListGet.PetsList[1] == personPetsSet.PetsList[1]))
                ViewBag.sessionVal = "True";
            else
                ViewBag.sessionVal = "False";

            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult SetNullObject()
        {
            Person personSet = null;
            Session["PersonNull"] = personSet;
            Object objGet = Session["PersonNull"];
            Person personGet = objGet as Person;
            ViewBag.sessionVal = ((personGet == null) && (objGet == null)) ? "OK" : "KO";
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult GetNullObject()
        {
            var objGet = Session["PersonNull"];
            Person personGet = Session["PersonNull"] as Person;
            ViewBag.sessionVal = ((personGet == null) && (objGet == null)) ? "OK" : "KO";
            return View("~/Views/Home/Index.aspx");
        }

        public ActionResult GetNonExistingKey()
        {
            var obj = Session["NonExistingKey"];
            ViewBag.sessionVal = (obj == null) ? "OK" : "KO";
            return View("~/Views/Home/Index.aspx");
        }
    }
}
