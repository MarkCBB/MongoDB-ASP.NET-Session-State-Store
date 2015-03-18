using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestApplicationv2_0.Models;

namespace TestApplicationv2_0.WebFormTests
{
    public partial class GetValues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Result1Literal.Text = Session.Mongo<int>(SetTwoValues.KEY_NAME).ToString();
            Result2Literal.Text = Session.Mongo<double>(SetTwoValues.KEY_NAME2).ToString();
            Person p = Session.Mongo<Person>(SetTwoValues.KEY_NAME3);
            Result3Literal.Text = string.Format("Name: {0}, surname: {1}", p.Name, p.Surname);
        }
    }
}