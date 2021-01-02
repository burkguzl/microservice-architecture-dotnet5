using Phonebook.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Phonebook.API.Repositories.Abstract
{
    public interface IPersonRepository
    {
        Task CreateAsync(Person person);
        Task DeleteAsync(string id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person> GetAsync(string id);
    }
}
