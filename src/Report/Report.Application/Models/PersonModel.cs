using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Application.Models
{
    public class PersonModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public List<AddressModel> Addresses { get; set; }
    }
}
