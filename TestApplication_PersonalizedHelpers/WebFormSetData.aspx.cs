using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestApplication_PersonalizedHelpers
{
    public partial class WebFormSetData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Mongo<string>("PersonalizedHelperForms", "Sample string");
            sessionVal.Text = sessionVal.Text = "<sessionVal>" + Session.Mongo<string>("PersonalizedHelperForms") + "</sessionVal>";
        }
    }
}