using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopeeFood.BLL.DTOS.CustomerDTOs;
using ShopeeFood.BLL.ServicesContract.CustomerServicesContract;
using ShopeeFood.Infrastructure.Common.ApiServices;
using ShopeeFood.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.ApplicationServices.CustomerServices
{
    public class CustomerServices : BaseApiServices, ICustomerServices
    {
        public CustomerServices(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base(httpContextAccessor, configuration)
        {

        }

        public async Task<AppActionResult<CustomerResponseDto, ApiErrorResponse>> GetCustomerProfile(HttpContext httpContext, string email)
        {
            Logger.Info("BENGIN - Get customer profile");
            var response = new AppActionResult<CustomerResponseDto, ApiErrorResponse>();
            var apiUrl = _configuration["GetCustomerProfile"];
            try
            {
                RestServices.SetBearerAuthorization("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwiZW1haWwiOiJkYXR0ZXN0X2FwaUB5b3BtYWlsLmNvbSIsImp0aSI6IjVkN2ViYzFmLTA0ZmEtNDNmNi04NTliLWM5N2Q1YmQ3NmRmZiIsImV4cCI6MTc1MzUyMjQ3NywiaXNzIjoic2hvcGVlLWNsb25lLndlYmFwaSIsImF1ZCI6InNob3BlZS1jbG9uZS53ZWJhcHAifQ.WL5AiOS7a06Hwi0qRPtsdzNScJ7lugKx4WOpxiDo4ZU");
                var result = await RestServices.GetAsync<CustomerResponseDto, ApiErrorResponse>(null, $"{ApiDomain}{apiUrl}");
                if (result.IsSuccess)
                {
                    Logger.Info($"Get customer profile: ");
                    response.SetResult(result.Data);
                }
                else
                {
                    response.SetError(result.Error);
                    Logger.Info($"FAIL to get customer profile: . ErrorCode: {result.Error?.ErrorCode}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"FAIL to get customer profile: {ex.Message}");
                response.SetError(new ApiErrorResponse { Message = ex.Message });
            }
            finally
            {
                Logger.Debug($"END - GetCities.");
            }

            return response;
        }
    }
}
