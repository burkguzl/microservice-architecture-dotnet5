using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Events
{
    public class PrepareReportEvent
    {
        public Guid RequestId { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
    public class Address
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string AddressType { get; set; }
    }
}
