using Microsoft.AspNetCore.Http;
using ShopeeFood.BLL.DTOS.CustomerDTOs;
using ShopeeFood.BLL.RequestDTOs.CustomerRequestDto;
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

        Task<AppActionResult<CustomerLoginResponseDto, ApiErrorResponse>> Login(HttpContext httpContext, CustomerLoginRequestDto requestDto);

        Task<AppActionResult<CustomerAddressDto, ApiErrorResponse>> AddCustomerAddress(HttpContext httpContext, CustomerAddressRequestDto requestDto);

        Task<AppActionResult<CustomerAddressDto, ApiErrorResponse>> UpdateCustomerAddress(HttpContext httpContext, CustomerAddressRequestDto requestDto);
    }
}
