using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestApplicationv2_0.Models;

namespace TestApplicationv2_0.WebFormTests
{
    public partial class SetTwoValues : System.Web.UI.Page
    {
        public const string KEY_NAME = "aspx_value";
        public const string KEY_NAME2 = "aspx_value2";
        public const string KEY_NAME3 = "aspx_value3";

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Mongo<int>(KEY_NAME, 314);
            Session.Mongo<double>(KEY_NAME2, 3.14d);
            Person p = new Person() { Name = "Marc", Surname = "Cortada" };
            Session.Mongo<Person>(KEY_NAME3, p);
            this.ResultLiteral.Text = "OK"; 
        }
    }
}