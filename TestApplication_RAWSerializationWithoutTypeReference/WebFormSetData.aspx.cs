using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestApplication_RAWSerializationWithoutTypeReference
{
    public partial class WebFormSetData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //This is only for test purposes, do NOT set the helper class here.
            //Is recommended to do it in an App_Start class and call the method in Application_Start event.
            System.Web.Mvc.MongoSessionUserHelpersMvc.SetHelper(new SessionHelperPersonalized());
            Session.Mongo<string>("PersonalizedHelperForms", "Sample string");
            sessionVal.Text = Session.Mongo<string>("PersonalizedHelperForms");
        }
    }
}