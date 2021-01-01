using MongoDB.Driver;
using Phonebook.API.Data.Abstract;
using Phonebook.API.Entities;
using Phonebook.API.Settings.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.API.Data.Concrete
{
    public class PhonebookDbContext:IPhonebookDbContext
    {
        public PhonebookDbContext(IPhonebookDatabaseSettings phonebookDatabaseSettings)
        {
            var client = new MongoClient(phonebookDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(phonebookDatabaseSettings.DatabaseName);
            Persons = database.GetCollection<Person>(phonebookDatabaseSettings.CollectionName);

            //seed data
            PhonebookDbSeedData.SeedData(Persons);
            
        }
        public IMongoCollection<Person> Persons { get; set; }
    }
}
