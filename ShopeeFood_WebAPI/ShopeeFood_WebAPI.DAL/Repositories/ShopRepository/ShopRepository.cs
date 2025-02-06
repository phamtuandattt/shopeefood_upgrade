using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopeeFood_WebAPI.DAL.IRepositories.IShopRepository;
using ShopeeFood_WebAPI.DAL.ModelResonposeDtos.ShopResponseDtos;
using ShopeeFood_WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.DAL.Repositories.ShopRepository
{
    public class ShopRepository : IShopRepository
    {
        private readonly ShopeefoodDbContext _context;
        private readonly IMapper _mapper;

        public ShopRepository(ShopeefoodDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ShopInfoResponseDto>> GetShopInfo(int shopID)
        {
            var infoShop = await _context.MenuShops
                .Where(s => s.ShopId == shopID)
                .Include(c => c.MenuDetailShops)
                .ToListAsync();

            if (infoShop == null)
            {
                return null;
            }
            
            return _mapper.Map<List<ShopInfoResponseDto>>(infoShop);
        }
    }
}
