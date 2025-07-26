using Microsoft.AspNetCore.Http;
using ShopeeFood.BLL.DTOS.CustomerDTOs;
using ShopeeFood.Infrastructure.Common.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.ServicesContract.CustomerServicesContract
{
    public interface ICustomerServices
    {
        Task<AppActionResult<CustomerResponseDto, ApiErrorResponse>> GetCustomerProfile(HttpContext httpContext, string email);
    }
}
