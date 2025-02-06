using ShopeeFood_WebAPI.DAL.ModelResonposeDtos.ShopResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.DAL.IRepositories.IShopRepository
{
    public interface IShopRepository
    {
        Task<List<ShopInfoResponseDto>> GetShopInfo(int shopID);
    }
}
