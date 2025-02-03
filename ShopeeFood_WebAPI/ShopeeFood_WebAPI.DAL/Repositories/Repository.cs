using Microsoft.EntityFrameworkCore;
using ShopeeFood_WebAPI.DAL.IRepositories;
using ShopeeFood_WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ShopeefoodDbContext _context;
        private DbSet<T> _dbSet;

        public Repository(ShopeefoodDbContext context)
        {
            _context = context;
            _dbSet = this._context.Set<T>();
        }

        public Task AddAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(List<T> list)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(List<T> list)
        {
            throw new NotImplementedException();
        }
    }
}
