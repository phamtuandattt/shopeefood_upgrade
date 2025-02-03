using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.DAL.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task AddAsync(T item);
        Task AddRangeAsync(List<T> list);
        Task<bool> UpdateAsync(T item);
        Task<bool> UpdateRangeAsync(List<T> list);
        Task<bool> DeleteAsync(object id);
        Task<bool> ExistsAsync(object id);
    }
}
