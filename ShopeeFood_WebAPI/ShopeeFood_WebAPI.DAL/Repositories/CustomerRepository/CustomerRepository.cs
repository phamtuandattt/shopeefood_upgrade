using AutoMapper;
using ShopeeFood_WebAPI.DAL.IRepositories;
using ShopeeFood_WebAPI.DAL.IRepositories.ICustomerRepository;
using ShopeeFood_WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.DAL.Repositories.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ShopeefoodDbContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(ShopeefoodDbContext shopeefoodDbContext, IMapper mapper)
        {
            _context = shopeefoodDbContext;
            _mapper = mapper;
        }

        public Task<Customer> GetCustomer(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
