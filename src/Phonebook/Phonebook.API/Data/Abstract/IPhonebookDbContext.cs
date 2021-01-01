using MongoDB.Driver;
using Phonebook.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.API.Data.Abstract
{
    public interface IPhonebookDbContext
    {
        IMongoCollection<Person> Persons { get; }
    }
}
