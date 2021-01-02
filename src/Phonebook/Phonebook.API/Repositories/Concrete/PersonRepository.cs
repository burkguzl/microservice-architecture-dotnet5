using MongoDB.Driver;
using Phonebook.API.Data.Abstract;
using Phonebook.API.Entities;
using Phonebook.API.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Phonebook.API.Repositories.Concrete
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IPhonebookDbContext _context;

        public PersonRepository(IPhonebookDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Person person)
        {
            await _context.Persons.InsertOneAsync(person);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Persons.DeleteOneAsync(Builders<Person>.Filter.ElemMatch(i => i.Id, id));
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {

            return await _context.Persons.Find(i => true).ToListAsync();

        }

        public async Task<Person> GetAsync(string id)
        {
            return await _context.Persons.Find(Builders<Person>.Filter.ElemMatch(i=> i.Id, id)).FirstOrDefaultAsync();
        }

       

        
    }
}
