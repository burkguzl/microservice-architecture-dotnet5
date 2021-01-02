using MongoDB.Bson;
using MongoDB.Driver;
using Phonebook.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.API.Data.Concrete
{
    public class PhonebookDbSeedData
    {
        public static void SeedData(IMongoCollection<Person> personCollection)
        {
            bool IsPersonExist = personCollection.Find(i => true).Any();
            if (!IsPersonExist)
            {
                personCollection.InsertManyAsync(new List<Person>
                {
                    new Person
                    {
                        Id = ObjectId.GenerateNewId().ToString().ToString(),
                         FirstName = "Burak",
                          LastName = "Güzel",
                           Company = "Sample",
                            Addresses = new List<Address>
                            {
                                 new Address
                                 {
                                     Id = ObjectId.GenerateNewId().ToString(),
                                      AddressType = "Home",
                                       Email = "burakguzel@outlook.com",
                                        Location = "Istanbul",
                                         Phone = "05555555555"
                                 },
                                  new Address
                                  {
                                      Id = ObjectId.GenerateNewId().ToString(),
                                      AddressType = "Work",
                                       Email = "burakguzel@outlook.com",
                                        Location = "Istanbul",
                                         Phone = "05555555555"
                                  }
                            }
                    },
                    new Person
                    {
                       Id = ObjectId.GenerateNewId().ToString(),
                         FirstName = "Ali Cem",
                          LastName = "Kirlibal",
                           Company = "Sample",
                            Addresses = new List<Address>
                            {
                                 new Address
                                 {
                                     Id = ObjectId.GenerateNewId().ToString(),
                                      AddressType = "Home",
                                       Email = "alicem@outlook.com",
                                        Location = "Istanbul",
                                         Phone = "05555555555"
                                 }

                            }
                    },
                    new Person
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                         FirstName = "Mert",
                          LastName = "Kesgin",
                           Company = "Sample",
                            Addresses = new List<Address>
                            {
                                 new Address
                                 {
                                     Id = ObjectId.GenerateNewId().ToString(),
                                      AddressType = "Home",
                                       Email = "mert@outlook.com",
                                        Location = "Ankara",
                                         Phone = "05555555555"
                                 }

                            }
                    },
                    new Person
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                         FirstName = "Selinay",
                          LastName = "Korkmaz",
                           Company = "Sample",
                            Addresses = new List<Address>
                            {
                                 new Address
                                 {
                                     Id = ObjectId.GenerateNewId().ToString(),
                                      AddressType = "Home",
                                       Email = "selinay@outlook.com",
                                        Location = "Istanbul",
                                         Phone = "05555555555"
                                 }

                            }
                    },
                     new Person
                    {
                         Id = ObjectId.GenerateNewId().ToString(),
                         FirstName = "Ayşe",
                          LastName = "Sağlam",
                           Company = "Sample",
                            Addresses = new List<Address>
                            {
                                 new Address
                                 {
                                     Id = ObjectId.GenerateNewId().ToString(),
                                      AddressType = "Home",
                                       Email = "ayse@outlook.com",
                                        Location = "Istanbul",
                                         Phone = "05555555555"
                                 }

                            }
                    },
                      new Person
                    {
                          Id = ObjectId.GenerateNewId().ToString(),
                         FirstName = "Selin",
                          LastName = "Akça",
                           Company = "Sample",
                            Addresses = new List<Address>
                            {
                                 new Address
                                 {
                                     Id = ObjectId.GenerateNewId().ToString(),
                                      AddressType = "Home",
                                       Email = "selin@outlook.com",
                                        Location = "Istanbul",
                                         Phone = "05555555555"
                                 }

                            }
                    }
                });
            }
        }
    }
}
