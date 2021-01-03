using EventBusRabbitMQ.Constants;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Events.Models;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Mvc;
using Phonebook.API.Entities;
using Phonebook.API.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Phonebook.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly EventBusRabbitMQProducer _eventBusRabbitMQProducer;

        public PersonController(IPersonRepository personRepository, EventBusRabbitMQProducer eventBusRabbitMQProducer)
        {
            _personRepository = personRepository;
            _eventBusRabbitMQProducer = eventBusRabbitMQProducer;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreatePerson(Person person)
        {
            await _personRepository.CreateAsync(person);
            return Ok();

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeletePerson(string id)
        {
            await _personRepository.DeleteAsync(id);
            return Ok();

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]  
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]  
        public async Task<ActionResult<Person>> GetPerson(string id)
        {
            var response = await _personRepository.GetAsync(id);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);

        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Person>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Person>>> GetAllPersons()
        {
            var response = await _personRepository.GetAllAsync();
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);

        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<Person>>> PrepareReport()
        {
            var persons = await _personRepository.GetAllAsync();
            var publishModel = new PrepareReportEvent();
            for (int i = 0; i < persons.Count(); i++)
            {
                publishModel.Persons = persons.Select(i => new PersonModel
                {
                    Id = i.Id,
                    Company = i.Company,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Addresses = i.Addresses.Select(x => new AddressModel
                    {
                        Id = x.Id,
                        AddressType = x.AddressType,
                        Email = x.Email,
                        Location = x.Location,
                        Phone = x.Phone
                    }).ToList()
                }).ToList();
            }

            publishModel.RequestId = Guid.NewGuid();

            try
            {
                _eventBusRabbitMQProducer.PublishPrepareReport(EventBusConstants.PrepareReportQueue, publishModel);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Ok();
        }



    }
}
