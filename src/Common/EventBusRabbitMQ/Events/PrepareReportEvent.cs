using EventBusRabbitMQ.Events.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Events
{
    public class PrepareReportEvent
    {
        public PrepareReportEvent()
        {
            Persons = new List<PersonModel>();
        }
        public Guid RequestId { get; set; }
        public List<PersonModel> Persons { get; set; }
        public string Location { get; set; }
    }
   
}
