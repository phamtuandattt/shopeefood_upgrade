using AutoMapper;
using ShopeeFood_WebAPI.BLL.Dtos;
using ShopeeFood_WebAPI.BLL.IServices;
using ShopeeFood_WebAPI.DAL.IRepositories;
using ShopeeFood_WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<bool> CityExisted(int cityId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int cityId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CityDto>> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return _mapper.Map<List<CityDto>>(items);
        }

        public Task<CityDto> GetById(int cityId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CityDto cityDto)
        {
            throw new NotImplementedException();
        }
    }
}
