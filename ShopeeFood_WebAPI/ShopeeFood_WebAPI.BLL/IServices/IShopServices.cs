using ShopeeFood_WebAPI.BLL.Dtos.ShopDtos;
using ShopeeFood_WebAPI.DAL.ModelResonposeDtos.ShopResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.BLL.IServices
{
    public interface IShopServices
    {
        Task<ShopInfoDto> GetShopInfo(int shopID);
        Task<List<ShopInfoResponseDto>> GetShopMenu(int shopID);
    }
}
