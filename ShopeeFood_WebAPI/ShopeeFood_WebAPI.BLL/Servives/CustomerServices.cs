using AutoMapper;
using log4net.Repository.Hierarchy;
using ShopeeFood_WebAPI.BLL.Dtos;
using ShopeeFood_WebAPI.BLL.Dtos.CustomerDtos;
using ShopeeFood_WebAPI.BLL.IServices;
using ShopeeFood_WebAPI.DAL.IRepositories;
using ShopeeFood_WebAPI.DAL.IRepositories.ICustomerRepository;
using ShopeeFood_WebAPI.DAL.Models;
using ShopeeFood_WebAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Logger = ShopeeFood_WebAPI.Infrastructure.Logger;

namespace ShopeeFood_WebAPI.BLL.Servives
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IRepository<Customer> _repository;
        private readonly IRepository<CustomerAddress> _customerAddressRepo;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerServices(IRepository<Customer> repository, IRepository<CustomerAddress> customerAddressRepo, ICustomerRepository customerRepository, IMapper mapper)
        {
            _repository = repository;
            _customerAddressRepo = customerAddressRepo;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CustomerDto customer)
        {
            try
            {
                var cus = _mapper.Map<Customer>(customer);
                await _repository.AddAsync(cus);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public async Task<CustomerAddressDto> AddCustomerAddressAsync(CustomerAddressDto customerAddress)
        {
            try
            {
                var address = _mapper.Map<CustomerAddress>(customerAddress);
                await _customerAddressRepo.AddAsync(address);
                return _mapper.Map<CustomerAddressDto>(address);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return new CustomerAddressDto();
        }

        public async Task DeleteAddressAsync(string email, int addressId)
        {
            var address = await _customerRepository.GetAddressByIdAsync(addressId);
            if (address.Customer != null && address.Customer.Email == email)
            {
                await _customerRepository.DeleteAddressAsync(address);
            }
            if (address == null)
            {
                Logger.Error("Address not found.");
            }
            if (address.Customer == null || address.Customer.Email != email)
            {
                Logger.Error("You are not authorized to delete this address.");
            }
        }

        public async Task<bool> Existed(string email)
        {
            return await _repository.ExistsAsync(e => e.Email == email);
        }

        public async Task<CustomerDto> GetCustomer(int id)
        {
            try
            {
                var customer = await _repository.GetByIdAsync(id);
                return _mapper.Map<CustomerDto>(customer);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return new CustomerDto();
        }

        public async Task<List<CustomerAddressDto?>> GetCustomerAddressByEmail(string email)
        {
            try
            {
                var addesses = await _customerRepository.GetCustomerAddressesByEmailAsync(email);
                var reponse = _mapper.Map<List<CustomerAddressDto>>(addesses);

                return reponse;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return new List<CustomerAddressDto?>();
        }

        public async Task<CustomerDto> GetCustomerByEmail(string email)
        {
            try
            {
                var customer = await _repository.FindOneAsync(e => e.Email == email);
                return _mapper.Map<CustomerDto>(customer);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            return new CustomerDto();
        }

        public async Task<CustomerDto?> GetCustomerWithDetailsByEmailAsync(string email)
        {
            // Can be use either ICustomerRepository or IRepositoy<Customer> 

            //var responseFromRepository = await _customerRepository.GetCustomerProfile(email);
            var responseFromCusRepo = await _repository.GetWithIncludesAsync<Customer>(
                c => c.Email == email,
                c => c.CustomerAddresses,
                c => c.CustomerExternalLogins
            );

            return _mapper.Map<CustomerDto>(responseFromCusRepo);
        }

        public async Task<bool> UpdateCustomer(int cusId, CustomerDto customer)
        {
            var item = await _repository.GetByIdAsync(cusId);
            if (item == null)
            {
                return false;
            }
            var item_n = _mapper.Map(customer, item);
            try
            {
                await _repository.UpdateAsync(item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<CustomerAddressDto> UpdateCustomerAddress(CustomerAddressDto customerAddress)
        {
            var item = await _customerAddressRepo.GetByIdAsync(customerAddress.AddressId);
            if (item == null)
            {
                return new CustomerAddressDto();
            }
            var item_n = _mapper.Map(customerAddress, item);
            try
            {
                await _customerAddressRepo.UpdateAsync(item_n);
                return _mapper.Map<CustomerAddressDto>(item_n);
            }
            catch (Exception)
            {
                return new CustomerAddressDto();
            }
        }
    }
}
