using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplicationv2_0.Models;

namespace TestApplicationv2_0.Controllers
{
    public class BsonDocumentController : Controller
    {
        //
        // GET: /BsonDocument/

        public ActionResult SetPerson()
        {
            //var doc = BsonDocument.Create(new Person() { Name = "Marc", Surname = "Cortada", City = "Barcelona" });
            BsonDocument doc = new BsonDocument();
            doc.Add("Name", "Marc");
            doc.Add("Surname", "Cortada");
            doc.Add("City", "Barcelona");
            Session.Mongo<BsonDocument>("BsonValueKey", doc);
            return View("~/Views/DefaultWithHelpers/Index.aspx");
        }

        public ActionResult GetPerson()
        {
            var doc = Session.Mongo<BsonDocument>("BsonValueKey");
            ViewBag.Name = doc.GetValue("Name");
            ViewBag.Surname = doc.GetValue("Surname");
            ViewBag.City = doc.GetValue("City");
            return View();
        }
    }
}
