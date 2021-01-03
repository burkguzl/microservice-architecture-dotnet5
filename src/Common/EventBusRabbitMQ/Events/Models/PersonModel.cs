using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Events.Models
{
    public class PersonModel
    {
        public PersonModel()
        {
            Addresses = new List<AddressModel>();
        }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public List<AddressModel> Addresses { get; set; }
    }


}
