using ShopeeFood_WebAPI.DAL.ModelResonposeDtos.CityResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.DAL.IRepositories.ICityRepository
{
    public interface ICityRepository
    {
        Task<List<CityBusinessFieldResponseDto>> GetCityBusinessFields(int cityID);
        Task<List<ShopInCityResponseDto>> GetShopInCities(int cityID, int fieldID, int pageNumber, int pageSize);
    }
}
