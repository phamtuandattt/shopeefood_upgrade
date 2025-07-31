using ShopeeFood_WebAPI.BLL.Dtos.CustomerDtos;
using ShopeeFood_WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.BLL.IServices
{
    public interface ICustomerServices
    {
        Task<CustomerDto> GetCustomer(int id);
        Task AddAsync(CustomerDto customer); 
        Task<CustomerDto> GetCustomerByEmail(string email);
        Task<bool> Existed(string email);
        
        Task<bool> UpdateCustomer(int cusId, CustomerDto customer);
        Task<CustomerDto?> GetCustomerWithDetailsByEmailAsync(string email);

        // Customer address
        Task<CustomerAddressDto> AddCustomerAddressAsync(CustomerAddressDto customerAddress);
        Task<CustomerAddressDto> UpdateCustomerAddress(CustomerAddressDto customerAddress);
        Task<List<CustomerAddressDto?>> GetCustomerAddressByEmail(string email);
        Task DeleteAddressAsync(string email, int addressId);

    }
}
