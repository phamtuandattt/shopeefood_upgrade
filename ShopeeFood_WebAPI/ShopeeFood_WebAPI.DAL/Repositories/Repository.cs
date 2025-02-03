using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopeeFood_WebAPI.DAL.IRepositories;
using ShopeeFood_WebAPI.DAL.Models;
using ShopeeFood_WebAPI.Infrastructure;
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
                throw new RepositoryException($"An error occurred while adding the entity to the database, {typeof(T).Name}", dbEx);
            }
            catch (Exception ex)
            {
                Logger.Error($"An unexpected error occurred while adding entity - {typeof(T).Name}");
                throw new RepositoryException($"An unexpected error occurred while adding the entity - {typeof(T).Name}", ex);
            }
        }

        public async Task AddRangeAsync(List<T> list)
        {
            if (list == null || !list.Any())
            {
                Logger.Error($"The list of entities cannot be null or empty");
                throw new RepositoryException($"An error occurred while adding the entity to the database, {typeof(T).Name}");
            }
            try
            {
                await _dbSet.AddRangeAsync(list);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Logger.Error($"Database update failed while adding {typeof(T).Name} entity - {dbEx?.InnerException?.Message.ToString()}");
                throw new RepositoryException($"An error occurred while adding the entity to the database, {typeof(T).Name}", dbEx);
            }
            catch (Exception ex)
            {
                Logger.Error($"An unexpected error occurred while adding entity - {typeof(T).Name}");
                throw new RepositoryException($"An unexpected error occurred while adding the entity - {typeof(T).Name}", ex);
            }
        }

        public async Task DeleteAsync(object id)
        {
            var item = await _dbSet.FindAsync(id);

            if (item == null)
            {
                Logger.Error($"Entity of type {typeof(T).Name} with the specified key not found.");
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with the specified key not found.");
            }

            try
            {
                // Remove entity
                _dbSet.Remove(item);

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbConcurrencyEx)
            {
                Logger.Error($"Database update failed while deleting  {typeof(T).Name} entity - ID: {id} - {dbConcurrencyEx?.InnerException?.Message.ToString()}");
                throw new RepositoryException($"Concurrency error occurred while deleting the entity - {typeof(T).Name}  - {dbConcurrencyEx?.InnerException?.Message.ToString()}");
            }
            catch (DbUpdateException dbEx)
            {
                Logger.Error("Database update failed while deleting entity.");
                throw new RepositoryException($"An error occurred while trying to delete the entity - {typeof(T).Name}", dbEx);
            }
        }

        public async Task<bool> ExistsAsync(object id)
        {
            var item = await _dbSet.FindAsync(id);
            if (item == null)
            {
                Logger.Error($"Entity of type {typeof(T).Name} with id {id} not found.");
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with id {id} not found.");
            }
            return item != null;
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
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with id {id} not found.");
            }
            return item;
        }

        public async Task UpdateAsync(T item)
        {
            var existingItem = await _dbSet.FindAsync(GetPrimaryKeyValue(item));

            if (existingItem == null)
            {
                Logger.Error($"Entity of type {typeof(T).Name} with the specified key not found.");
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with the specified key not found.");
            }

            try
            {
                _context.Entry(existingItem).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbConcurrencyEx)
            {
                Logger.Error($"Concurrency error occurred while updating the entity - {typeof(T).Name}  - {dbConcurrencyEx?.InnerException?.Message.ToString()}");
                throw new RepositoryException($"Concurrency error occurred while updating the entity - {typeof(T).Name}  - {dbConcurrencyEx?.InnerException?.Message.ToString()}");
            }
            catch (DbUpdateException dbEx)
            {
                Logger.Error($"An error occurred while trying to update the entity - {typeof(T).Name}", dbEx);
                throw new RepositoryException($"An error occurred while trying to update the entity - {typeof(T).Name}", dbEx);
            }
        }

        public Task UpdateRangeAsync(List<T> list)
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
