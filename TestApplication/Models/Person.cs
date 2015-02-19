using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApplication.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
    }

    public class PersonPetsList : Person
    {
        public List<string> PetsList { get; set; }
    }
}