using Microsoft.AspNetCore.Http;
using ShopeeFood.BLL.DTOS.PageSettingDTOs;
using ShopeeFood.Infrastructure.Common.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.ServicesContract.PageSettingServicesContract
{
    public interface IPageSettingServices
    {
        Task<AppActionResult<List<PageSettingDto>, ApiErrorResponse>> GetPageSettings(HttpContext httpContext);
    }
}
