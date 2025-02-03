using AutoMapper;
using log4net.Repository.Hierarchy;
using ShopeeFood_WebAPI.BLL.Dtos;
using ShopeeFood_WebAPI.BLL.IServices;
using ShopeeFood_WebAPI.DAL.IRepositories;
using ShopeeFood_WebAPI.DAL.Models;
using ShopeeFood_WebAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.BLL.Servives
{
    public class CityServices : ICityServies
    {
        private readonly IRepository<City> _repository;
        private readonly IMapper _mapper;

        public CityServices(IRepository<City> repository, IMapper mapper) 
        {
            this._repository = repository;        
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

        public async Task<CityDto> GetById(int cityId)
        {
            var item = await _repository.GetByIdAsync(cityId);
            return _mapper.Map<CityDto>(item) ?? new CityDto();
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
