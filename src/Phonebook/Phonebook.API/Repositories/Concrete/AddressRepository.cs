using MongoDB.Bson;
using MongoDB.Driver;
using Phonebook.API.Data.Abstract;
using Phonebook.API.Entities;
using Phonebook.API.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.API.Repositories.Concrete
{
    public class AddressRepository:IAddressRepository
    {
        private readonly IPhonebookDbContext _context;

        public AddressRepository(IPhonebookDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(string personId, Address address)
        {

            var person = await _context.Persons.Find(i => i.Id == personId).FirstOrDefaultAsync();

            person.Addresses.Add(address);

            await _context.Persons.FindOneAndUpdateAsync(i=> i.Id == personId, Builders<Person>.Update.Set(i=> i.Addresses, person.Addresses));

        }

        public async Task DeleteAsync(string personId, string addressId)
        {
            var person = await _context.Persons.Find(i => i.Id == personId).FirstOrDefaultAsync();

            var deleteAddress = person.Addresses.FirstOrDefault(i => i.Id == addressId);

            person.Addresses.Remove(deleteAddress);

            await _context.Persons.FindOneAndUpdateAsync(i => i.Id == personId, Builders<Person>.Update.Set(i=> i.Addresses , person.Addresses));
        }
    }
}
