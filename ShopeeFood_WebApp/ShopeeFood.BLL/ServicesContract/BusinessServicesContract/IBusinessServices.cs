using Microsoft.AspNetCore.Http;
using ShopeeFood.BLL.DTOS.BusinessDTOs;
using ShopeeFood.BLL.DTOS.CityDTOs;
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
        Task<AppActionResult<IEnumerable<CityDto>, ApiErrorResponse>> GetAllByCity(HttpContext httpContext, int cityId);
    }
}
