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
        public const string KEY_NAME4 = "aspx_value4";

        protected void Page_Load(object sender, EventArgs e)
        {
            int intValue = 314;
            double doubleValue = 3.14d;
            Person personValue = new Person() { Name = "Marc", Surname = "Cortada" };
            PersonPetsList personPetsValue = new PersonPetsList()
            {
                Name = "Marc2",
                Surname = "Cortada2",
                PetsList = new List<string>() { "cat", "dog" }
            };

            Session.Mongo<int>(KEY_NAME, intValue);
            Session.Mongo<double>(KEY_NAME2, doubleValue);
            Session.Mongo<Person>(KEY_NAME3, personValue);
            Session.Mongo<PersonPetsList>(KEY_NAME4, personPetsValue);

            if ((intValue == Session.Mongo<int>(KEY_NAME)) &&
                (doubleValue == Session.Mongo<double>(KEY_NAME2)) &&
                (personValue.Name == Session.Mongo<Person>(KEY_NAME3).Name) && 
                (personValue.Surname == Session.Mongo<Person>(KEY_NAME3).Surname) &&
                (personPetsValue.Name == Session.Mongo<PersonPetsList>(KEY_NAME4).Name) &&
                (personPetsValue.Surname == Session.Mongo<PersonPetsList>(KEY_NAME4).Surname) &&
                (personPetsValue.PetsList[0] == Session.Mongo<PersonPetsList>(KEY_NAME4).PetsList[0]) &&
                (personPetsValue.PetsList[1] == Session.Mongo<PersonPetsList>(KEY_NAME4).PetsList[1]))
                this.ResultLiteral.Text = "OK";
            else
                this.ResultLiteral.Text = "KO";
        }
    }
}