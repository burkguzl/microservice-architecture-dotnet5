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
    public class AddressController:ControllerBase
    {
        private readonly IAddressRepository _addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpPost("{personId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAddress(string personId, Address address)
        {
            await _addressRepository.CreateAsync(personId, address);

            return Ok();

        }
        [HttpDelete("{personId}/{addressId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAddress(string personId, string addressId)
        {
            await _addressRepository.DeleteAsync(personId, addressId);

            return Ok();

        }
    }
}
