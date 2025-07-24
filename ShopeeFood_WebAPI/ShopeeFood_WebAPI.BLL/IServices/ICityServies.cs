using ShopeeFood_WebAPI.BLL.Dtos;
using ShopeeFood_WebAPI.BLL.Dtos.CityDtos;
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
        Task<bool> UpdateAsync(int id, CityDto cityDto);
        Task<bool> DeleteAsync(int cityId);
        Task<bool> CityExisted(int cityId);
        Task<List<CityBusinessDto>> GetAllBusiness(int cityID);
        Task<List<ShopInTheCityDto>> GetShopInTheCity(int cityID, int fieldID, int pageNumber, int pageSize);
        Task<ShopCityBusinessResponseDto> GetShopCityBusiness(int cityID, int fieldID, int pageNumber, int pageSize);
    }
}
