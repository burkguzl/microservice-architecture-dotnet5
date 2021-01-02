using Phonebook.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.API.Repositories.Abstract
{
    public interface IAddressRepository
    {
        Task CreateAsync(string personId, Address address);
        Task DeleteAsync(string personId, string addressId);
    }
}
