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
            FilterDefinition<Person> filter = Builders<Person>.Filter.ElemMatch(i => i.Id, personId);

            var person = await _context.Persons.FindAsync(filter).Result.FirstOrDefaultAsync();

            person.Addresses.Add(address);

            UpdateDefinition<Person> update = Builders<Person>.Update.Set(i => i.Addresses, person.Addresses);

            await _context.Persons.FindOneAndUpdateAsync(filter, update);
        }

        public async Task DeleteAsync(string personId, string addressId)
        {
            FilterDefinition<Person> filter = Builders<Person>.Filter.ElemMatch(i => i.Id, personId);

            var person = await _context.Persons.FindAsync(filter).Result.FirstOrDefaultAsync();

            var address = person.Addresses.FirstOrDefault(i => i.Id == addressId);

            person.Addresses.Remove(address);

            UpdateDefinition<Person> update = Builders<Person>.Update.Set(i => i.Addresses, person.Addresses);

            await _context.Persons.FindOneAndUpdateAsync(filter, update);
        }
    }
}
