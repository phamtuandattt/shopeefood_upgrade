using ShopeeFood.BLL.DTOS.ShopDTOs;
using ShopeeFood.BLL.RequestDTOs.ShopRequestDTOs;
using ShopeeFood.Infrastructure.Common.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.ServicesContract.ShopServicesContract
{
    public interface IShopServices
    {
        Task<AppActionResult<IEnumerable<ShopResponseDtos>, ApiErrorResponse>> GetShopOfCityByBusinessField(ShopRequestDto request);
    }
}
