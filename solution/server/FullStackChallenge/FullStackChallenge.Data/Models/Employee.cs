using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackChallenge.Data.Models
{
    public class Employee : IModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
