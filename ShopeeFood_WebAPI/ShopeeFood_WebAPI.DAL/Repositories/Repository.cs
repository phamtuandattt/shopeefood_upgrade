using log4net.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopeeFood_WebAPI.DAL.IRepositories;
using ShopeeFood_WebAPI.DAL.Models;
using ShopeeFood_WebAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task AddAsync(T item)
        {
            try
            {
                await _dbSet.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Logger.Error($"Database update failed while adding {typeof(T).Name} entity - {dbEx?.InnerException?.Message.ToString()}");
            }
            catch (Exception ex)
            {
                Logger.Error($"An unexpected error occurred while adding entity - {typeof(T).Name}");
            }
        }

        public async Task AddRangeAsync(List<T> list)
        {
            if (list == null || !list.Any())
            {
                Logger.Error($"The list of entities cannot be null or empty");
            }
            try
            {
                await _dbSet.AddRangeAsync(list);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Logger.Error($"Database update failed while adding {typeof(T).Name} entity - {dbEx?.InnerException?.Message.ToString()}");
            }
            catch (Exception ex)
            {
                Logger.Error($"An unexpected error occurred while adding entity - {typeof(T).Name}");
            }
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var item = await _dbSet.FindAsync(id);

            if (item == null)
            {
                Logger.Error($"Entity of type {typeof(T).Name} with the specified key not found.");
                return false;
            }

            try
            {
                // Remove entity
                _dbSet.Remove(item);

                // Save changes to the database
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException dbConcurrencyEx)
            {
                Logger.Error($"Database update failed while deleting  {typeof(T).Name} entity - ID: {id} - {dbConcurrencyEx?.InnerException?.Message.ToString()}");
                return false;
            }
            catch (DbUpdateException dbEx)
            {
                Logger.Error("Database update failed while deleting entity.");
                return false;
            }
        }

        public async Task<bool> ExistsAsync(object id)
        {
            var item = await _dbSet.FindAsync(id);
            if (item == null)
            {
                Logger.Error($"Entity of type {typeof(T).Name} with id {id} not found.");
            }
            return item != null;
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T?> FindOneAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> GetAllAsync()
        {
            var items = await _dbSet.ToListAsync();
            if (items == null || !items.Any())
            {
                Logger.Error($"The {typeof(T).Name} no data available !");
                return new List<T>();  // Return an empty list if no items are found
            }
            return items;
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var item = await _dbSet.FindAsync(id);
            if (item == null)
            {
                Logger.Error($"Entity of type {typeof(T).Name} with id {id} not found.");
                return null;
            }
            return item;
        }

        public async Task<bool> UpdateAsync(T item)
        {
            var existingItem = await _dbSet.FindAsync(GetPrimaryKeyValue(item));

            if (existingItem == null)
            {
                Logger.Error($"Entity of type {typeof(T).Name} with the specified key not found.");
                return false;
            }

            try
            {
                _context.Entry(existingItem).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException dbConcurrencyEx)
            {
                Logger.Error($"Concurrency error occurred while updating the entity - {typeof(T).Name}  - {dbConcurrencyEx?.InnerException?.Message.ToString()}");
                return false;
            }
            catch (DbUpdateException dbEx)
            {
                Logger.Error($"An error occurred while trying to update the entity - {typeof(T).Name}", dbEx);
                return false;
            }
        }

        public Task<bool> UpdateRangeAsync(List<T> list)
        {
            throw new NotImplementedException();
        }

        private object GetPrimaryKeyValue(T entity)
        {
            var keyName = _context.Model?.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.Select(x => x.Name).Single();

            return entity.GetType().GetProperty(keyName)?.GetValue(entity, null);
        }
    }
}
