using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestApplication_RAWSerialization.Controllers
{
    public class GetWithoutTypeController : Controller
    {
        //
        // GET: /GetWithoutType/

        public ActionResult Index()
        {
            dynamic personGet = Session[HomeController.KEY_NAME];
            dynamic personPetsListGet = Session[HomeController.KEY_NAME3];

            // We use literal values because we don't have any reference to the
            // real object type
            if (("Marc" == personGet.Name) &&
                ("Cortada" == personGet.Surname) &&
                ("Barcelona" == personGet.City) &&
                ("Marc2" == personPetsListGet.Name) &&
                ("Cortada2" == personPetsListGet.Surname) &&
                ("cat" == personPetsListGet.PetsList[0]) &&
                ("dog" == personPetsListGet.PetsList[1]))
                ViewBag.sessionVal = "True";
            else
                ViewBag.sessionVal = "False";

            return View("~/Views/Home/Index.aspx");
        }

    }
}
