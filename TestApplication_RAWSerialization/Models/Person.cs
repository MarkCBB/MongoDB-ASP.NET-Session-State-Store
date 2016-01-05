using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApplication_RAWSerialization.Models
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
    }

    [Serializable]
    public class PersonPetsList : Person
    {
        public List<string> PetsList { get; set; }
    }
}