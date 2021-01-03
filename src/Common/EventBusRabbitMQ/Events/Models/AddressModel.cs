using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Events.Models
{
    public class AddressModel
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string AddressType { get; set; }
    }
}
