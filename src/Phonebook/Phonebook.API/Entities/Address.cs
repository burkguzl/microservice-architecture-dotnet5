using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.API.Entities
{
    public class Address
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string AddressType { get; set; }
    }
}
