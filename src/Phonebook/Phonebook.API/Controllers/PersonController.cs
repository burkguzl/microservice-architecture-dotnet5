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

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreatePerson(Person person)
        {
            await _personRepository.CreateAsync(person);
            return Ok();

        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeletePerson(string id)
        {
            await _personRepository.DeleteAsync(id);
            return Ok();

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
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
        


    }
}
