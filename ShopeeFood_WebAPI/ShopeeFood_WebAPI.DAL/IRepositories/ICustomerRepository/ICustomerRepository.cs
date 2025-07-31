using ShopeeFood_WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.DAL.IRepositories.ICustomerRepository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerProfile(string email);

        Task<List<CustomerAddress>> GetCustomerAddressesByEmailAsync(string email);

        Task DeleteAddressAsync(CustomerAddress address);
        Task<CustomerAddress?> GetAddressByIdAsync(int addressId);
    }
}
