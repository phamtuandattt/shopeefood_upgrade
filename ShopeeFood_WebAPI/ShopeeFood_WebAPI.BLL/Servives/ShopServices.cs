using AutoMapper;
using ShopeeFood_WebAPI.BLL.Dtos.ShopDtos;
using ShopeeFood_WebAPI.BLL.IServices;
using ShopeeFood_WebAPI.DAL.IRepositories;
using ShopeeFood_WebAPI.DAL.IRepositories.IShopRepository;
using ShopeeFood_WebAPI.DAL.ModelResonposeDtos.ShopResponseDtos;
using ShopeeFood_WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.BLL.Servives
{
    public class ShopServices : IShopServices
    {
        private readonly IRepository<Shop> _repository;
        private readonly IShopRepository _shopRepo;
        private readonly IMapper _mapper;

        public ShopServices(IRepository<Shop> shopRepository, IShopRepository shopRepo, IMapper mapper)
        {
            _repository = shopRepository;
            _shopRepo = shopRepo;
            _mapper = mapper;
        }

        public async Task<ShopInfoDto> GetShopInfo(int shopID)
        {
            var item = await _repository.GetByIdAsync(shopID);

            return _mapper.Map<ShopInfoDto>(item) ?? new ShopInfoDto();
        }

        public async Task<List<ShopInfoResponseDto>> GetShopMenu(int shopID)
        {
            return await _shopRepo.GetShopInfo(shopID);
        }
    }
}
