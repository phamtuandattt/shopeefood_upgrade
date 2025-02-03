using ShopeeFood_WebAPI.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.BLL.IServices
{
    public interface ICityServies
    {
        Task<CityDto> GetById(int cityId);
        Task<List<CityDto>> GetAll();
        Task AddAsync(CityDto cityDto);
        Task UpdateAsync(CityDto cityDto);
        Task DeleteAsync(int cityId);
        Task<bool> CityExisted(int cityId);
    }
}
