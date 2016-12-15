using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
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
        public const string LONG_RUNNING_VALUE = "LongRunningValue";

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

        public ActionResult SetNullPersonValue()
        {
            Person setVal = null;
            Session.Mongo<Person>("NullPerson", setVal);
            Person getVal = Session.Mongo<Person>("NullPerson");
            ViewBag.sessionVal = (getVal == null) ? "OK" : "KO";
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult GetNullPersonValue()
        {
            Person getVal = Session.Mongo<Person>("NullPerson");
            ViewBag.sessionVal = (getVal == null) ? "OK" : "KO";
            return View("~/Views/Default/PrintSessionVal.aspx");
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

        public ActionResult SetSessionValNullableInt()
        {
            int? setVal = 3;
            Session.Mongo<int?>("nullableInt", setVal);
            int? getVal = Session.Mongo<int?>("nullableInt");
            ViewBag.sessionVal = getVal;
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult GetSessionValNullableInt()
        {
            int? getVal = Session.Mongo<int?>("nullableInt");
            ViewBag.sessionVal = getVal;
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult SetSessionValNullInt()
        {
            int? setVal = null;
            Session.Mongo<int?>("nullInt", setVal);
            int? getVal = Session.Mongo<int?>("nullInt");
            ViewBag.sessionVal = (getVal == null) ? "OK" : "KO";
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult GetSessionValNullInt()
        {
            int? getVal = Session.Mongo<int?>("nullInt");
            ViewBag.sessionVal = (getVal == null) ? "OK" : "KO";
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult SetSessionValNullableDouble()
        {
            double? setVal = 3.14d;
            Session.Mongo<double?>("NullableDouble", setVal);
            double? getVal = Session.Mongo<double?>("NullableDouble");
            ViewBag.sessionVal = getVal;
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult GetSessionValNullableDouble()
        {
            double? getVal = Session.Mongo<double?>("NullableDouble");
            ViewBag.sessionVal = getVal;
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult SetSessionValEnum()
        {
            ConsoleColor setVal = ConsoleColor.Black;
            Session.Mongo<ConsoleColor>("Enum", setVal);
            ConsoleColor getVal = Session.Mongo<ConsoleColor>("Enum");
            ViewBag.sessionVal = getVal;
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult GetSessionValEnum()
        {
            ConsoleColor getVal = Session.Mongo<ConsoleColor>("Enum");
            ViewBag.sessionVal = getVal.ToString();
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult SetSessionValNullableEnum()
        {
            ConsoleColor? setVal = ConsoleColor.DarkBlue;
            Session.Mongo<ConsoleColor?>("NullableEnum", setVal);
            ConsoleColor? getVal = Session.Mongo<ConsoleColor?>("NullableEnum");
            ViewBag.sessionVal = getVal;
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult GetSessionValNullableEnum()
        {
            ConsoleColor? getVal = Session.Mongo<ConsoleColor?>("NullableEnum");
            ViewBag.sessionVal = getVal.ToString();
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult SetSessionValNullableEnumToNull()
        {
            ConsoleColor? setVal = null;
            Session.Mongo<ConsoleColor?>("NullableEnumToNull", setVal);
            ConsoleColor? getVal = Session.Mongo<ConsoleColor?>("NullableEnumToNull");
            ViewBag.sessionVal = (getVal == null) ? "OK" : "KO";
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult GetSessionValNullableEnumToNull()
        {
            ConsoleColor? getVal = Session.Mongo<ConsoleColor?>("NullableEnumToNull");
            ViewBag.sessionVal = (getVal == null) ? "OK" : "KO";
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult SetSessionValNullableDecimal()
        {
            decimal? setVal = 3.14M;
            bool exceptionCaught = false;
            try
            {
                Session.Mongo<decimal?>("ValNullableDecimal", setVal);
            }
            catch (Exception)
            {
                exceptionCaught = true;
            }
            decimal? getVal = Session.Mongo<decimal?>("ValNullableDecimal");
            ViewBag.sessionVal = ((exceptionCaught == true) && (getVal == null)) ? "OK" : "KO";
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult GetSessionValNullableDecimal()
        {
            decimal? getVal = Session.Mongo<decimal?>("ValNullableDecimal");
            ViewBag.sessionVal = (getVal == null) ? "OK" : "KO";
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult SetSessionValDecimal()
        {
            decimal setVal = 3.14M;
            bool exceptionCaught = false;
            try
            {
                Session.Mongo<decimal>("ValDecimal", setVal);
            }
            catch (Exception)
            {
                exceptionCaught = true;
            }
            decimal getVal = Session.Mongo<decimal>("ValDecimal");
            ViewBag.sessionVal = ((exceptionCaught == true) && (getVal == 0)) ? "OK" : "KO";
            return View("~/Views/Default/PrintSessionVal.aspx");
        }

        public ActionResult GetSessionValDecimal()
        {
            decimal getVal = Session.Mongo<decimal>("ValDecimal");
            ViewBag.sessionVal = (getVal == 0) ? "OK" : "KO";
            return View("~/Views/Default/PrintSessionVal.aspx");
        }
        
        public ActionResult ShortTimeWriteProcess()
        {
            Session.Mongo("abc", 123);

            return new EmptyResult();
        }

        public ActionResult LongTimeWriteProcess()
        {
            int i = 0;
            DateTime end = DateTime.Now.AddSeconds(30);

            while (DateTime.Now <= end)
            {
                i++;                
                Session.Mongo<int>(LONG_RUNNING_VALUE, i);
                System.Threading.Thread.CurrentThread.Join(1000);
            }

            ViewBag.sessionVal = i;
            return View("~/Views/Default/PrintSessionVal.aspx");
        }
    }
}
