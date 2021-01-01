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
                         FirstName = "Burak",
                          LastName = "Güzel",
                           Company = "Sample",
                            Addresses = new List<Address>
                            {
                                 new Address
                                 {
                                      AddressType = "Home",
                                       Email = "burakguzel@outlook.com",
                                        Location = "Istanbul",
                                         Phone = "05555555555"
                                 },
                                  new Address
                                  {
                                      AddressType = "Work",
                                       Email = "burakguzel@outlook.com",
                                        Location = "Istanbul",
                                         Phone = "05555555555"
                                  }
                            }
                    },
                    new Person
                    {
                         FirstName = "Ali Cem",
                          LastName = "Kirlibal",
                           Company = "Sample",
                            Addresses = new List<Address>
                            {
                                 new Address
                                 {
                                      AddressType = "Home",
                                       Email = "alicem@outlook.com",
                                        Location = "Istanbul",
                                         Phone = "05555555555"
                                 }

                            }
                    },
                    new Person
                    {
                         FirstName = "Mert",
                          LastName = "Kesgin",
                           Company = "Sample",
                            Addresses = new List<Address>
                            {
                                 new Address
                                 {
                                      AddressType = "Home",
                                       Email = "mert@outlook.com",
                                        Location = "Ankara",
                                         Phone = "05555555555"
                                 }

                            }
                    },
                    new Person
                    {
                         FirstName = "Selinay",
                          LastName = "Korkmaz",
                           Company = "Sample",
                            Addresses = new List<Address>
                            {
                                 new Address
                                 {
                                      AddressType = "Home",
                                       Email = "selinay@outlook.com",
                                        Location = "Istanbul",
                                         Phone = "05555555555"
                                 }

                            }
                    },
                     new Person
                    {
                         FirstName = "Ayşe",
                          LastName = "Sağlam",
                           Company = "Sample",
                            Addresses = new List<Address>
                            {
                                 new Address
                                 {
                                      AddressType = "Home",
                                       Email = "ayse@outlook.com",
                                        Location = "Istanbul",
                                         Phone = "05555555555"
                                 }

                            }
                    },
                      new Person
                    {
                         FirstName = "Selin",
                          LastName = "Akça",
                           Company = "Sample",
                            Addresses = new List<Address>
                            {
                                 new Address
                                 {
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
