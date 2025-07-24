using AutoMapper;
using log4net.Repository.Hierarchy;
using ShopeeFood_WebAPI.BLL.Dtos;
using ShopeeFood_WebAPI.BLL.Dtos.CityDtos;
using ShopeeFood_WebAPI.BLL.IServices;
using ShopeeFood_WebAPI.DAL.IRepositories;
using ShopeeFood_WebAPI.DAL.IRepositories.ICityRepository;
using ShopeeFood_WebAPI.DAL.Models;
using ShopeeFood_WebAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.BLL.Servives
{
    public class CityServices : ICityServies
    {
        private readonly IRepository<City> _repository;
        private readonly ICityRepository _cityRepo;
        private readonly IMapper _mapper;

        public CityServices(IRepository<City> repository,ICityRepository cityRepo,  IMapper mapper) 
        {
            this._repository = repository;        
            this._cityRepo = cityRepo;
            this._mapper = mapper;
        }

        public async Task AddAsync(CityDto cityDto)
        {
            var item = _mapper.Map<City>(cityDto);
            await _repository.AddAsync(item);
        }

        public async Task<bool> CityExisted(int cityId)
        {
            return await _repository.ExistsAsync(cityId);
        }

        public async Task<bool> DeleteAsync(int cityId)
        {
            return await _repository.DeleteAsync(cityId);
        }

        public async Task<List<CityDto>> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return _mapper.Map<List<CityDto>>(items);
        }

        public async Task<List<CityBusinessDto>> GetAllBusiness(int cityID)
        {
            var items = await _cityRepo.GetCityBusinessFields(cityID);
            return _mapper.Map<List<CityBusinessDto>>(items);
        }

        public async Task<CityDto> GetById(int cityId)
        {
            var item = await _repository.GetByIdAsync(cityId);
            return _mapper.Map<CityDto>(item) ?? new CityDto();
        }

        public async Task<ShopCityBusinessResponseDto> GetShopCityBusiness(int cityID, int fieldID, int pageNumber, int pageSize)
        {
            var businesses = await _cityRepo.GetCityBusinessFields(cityID);
            var shops = await _cityRepo.GetShopInCities(cityID, fieldID, pageNumber, pageSize);
            if (businesses.Count > 0 && shops.Count > 0)
            {
                var response = new ShopCityBusinessResponseDto()
                {
                    CityBusinesses = _mapper.Map<List<CityBusinessDto>>(businesses.ToList()),
                    ShopInTheCities = _mapper.Map<List<ShopInTheCityDto>>(shops.ToList())
                };
                return response;
            }
            return new ShopCityBusinessResponseDto();
        }

        public async Task<List<ShopInTheCityDto>> GetShopInTheCity(int cityID, int fieldID, int pageNumber, int pageSize)
        {
            var items = await _cityRepo.GetShopInCities(cityID, fieldID, pageNumber, pageSize);
            return _mapper.Map<List<ShopInTheCityDto>>(items);
        }

        public async Task<bool> UpdateAsync(int id, CityDto cityDto)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return false;
            }
            var item_n = _mapper.Map(cityDto, item);
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
    }
}
