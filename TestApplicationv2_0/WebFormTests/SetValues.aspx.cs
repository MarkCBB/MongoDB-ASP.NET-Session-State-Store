using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoSessionStateStore.Helpers;

namespace TestApplicationv2_0.WebFormTests
{
    public partial class SetTwoValues : System.Web.UI.Page
    {
        public const string KEY_NAME = "aspx_value";
        public const string KEY_NAME2 = "aspx_value2";

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Mongo<int>(KEY_NAME, 314);
            Session.Mongo<double>(KEY_NAME2, 3.14d);
            this.ResultLiteral.Text = "OK"; 
        }
    }
}