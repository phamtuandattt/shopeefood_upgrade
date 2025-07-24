using Microsoft.AspNetCore.Http;
using ShopeeFood.BLL.DTOS.BusinessDTOs;
using ShopeeFood.BLL.DTOS.CityDTOs;
using ShopeeFood.BLL.RequestDTOs.ShopRequestDTOs;
using ShopeeFood.Infrastructure.Common.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.ServicesContract.BusinessServicesContract
{
    public interface IBusinessServices
    {
        Task<AppActionResult<IEnumerable<CityDto>, ApiErrorResponse>> GetAllByCity(HttpContext httpContext);
        Task<AppActionResult<IEnumerable<BusinessDto>, ApiErrorResponse>> GetBusinessByCity(HttpContext httpContext, int cityId);

        Task<AppActionResult<ShopCityBusinessResponseDto, ApiErrorResponse>> GetShopCityBusinesses(HttpContext httpContext, ShopRequestDto shopRequestDto);
    }
}
