using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopeeFood_WebAPI.DAL.IRepositories.ICityRepository;
using ShopeeFood_WebAPI.DAL.ModelResonposeDtos.CityResponseDtos;
using ShopeeFood_WebAPI.DAL.Models;
using ShopeeFood_WebAPI.DAL.SqlCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.DAL.Repositories.CityRepository
{
    public class CityRepository : ICityRepository
    {
        private readonly ShopeefoodDbContext _context;
        private readonly IMapper _mapper;

        public CityRepository(ShopeefoodDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CityBusinessFieldResponseDto>> GetCityBusinessFields(int cityID)
        {
            var sqlQuery = string.Format(StoreProcedure.GET_BUSINESS_IN_THE_CITY, cityID);
            var result = await _context.Database.SqlQueryRaw<CityBusinessFieldResponseDto>(sqlQuery).ToListAsync();
            return result;
        }

        public async Task<List<ShopInCityResponseDto>> GetShopInCities(int cityID, int fieldID, int pageNumber, int pageSize)
        {
            var sqlQuery = string.Format(StoreProcedure.GET_SHOP_BUSINESS_IN_THE_CITY, cityID, fieldID, pageNumber, pageSize);
            var result = await _context.ShopInCityResponseDtos.FromSqlRaw(sqlQuery).ToListAsync();
            return result;
        }
    }
}
