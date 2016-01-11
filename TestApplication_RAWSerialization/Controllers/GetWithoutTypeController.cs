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
            dynamic personGetted = Session[HomeController.KEY_NAME];
            dynamic personPetsListGetted = Session[HomeController.KEY_NAME3];
            dynamic gettedValInt = Session[HomeController.KEY_NAME2];

            // We use literal values because we don't have any reference to the
            // real object type
            if ((3 == gettedValInt) &&
                ("Marc" == personGetted.Name) &&
                ("Cortada" == personGetted.Surname) &&
                ("Barcelona" == personGetted.City) &&
                ("Marc2" == personPetsListGetted.Name) &&
                ("Cortada2" == personPetsListGetted.Surname) &&
                ("cat" == personPetsListGetted.PetsList[0]) &&
                ("dog" == personPetsListGetted.PetsList[1]))
                ViewBag.sessionVal = "True";
            else
                ViewBag.sessionVal = "False";

            return View("~/Views/Home/Index.aspx");
        }

    }
}
