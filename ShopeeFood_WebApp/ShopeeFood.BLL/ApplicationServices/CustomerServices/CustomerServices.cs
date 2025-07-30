using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopeeFood.BLL.DTOS.CustomerDTOs;
using ShopeeFood.BLL.RequestDTOs.CustomerRequestDto;
using ShopeeFood.BLL.ServicesContract.CustomerServicesContract;
using ShopeeFood.Infrastructure.Common.ApiServices;
using ShopeeFood.Infrastructure.Common.SessionManagement;
using ShopeeFood.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.WebSockets;
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
            var clientSession = new ClientSession(_httpContextAccessor);
            try
            {
                RestServices.SetBearerAuthorization(clientSession.AccessToken);
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

        public async Task<AppActionResult<CustomerLoginResponseDto, ApiErrorResponse>> Login(HttpContext httpContext, CustomerLoginRequestDto requestDto)
        {
            Logger.Info("BENGIN - Login");
            var response = new AppActionResult<CustomerLoginResponseDto, ApiErrorResponse>();
            var apiUrl = _configuration["UserLogin"];
            var clientSession = new ClientSession(_httpContextAccessor.HttpContext);
            try
            {
                if (requestDto != null)
                {
                    var postData = SerializeParams(requestDto);
                    var result = await RestServices.PostAsync<CustomerLoginResponseDto, ApiErrorResponse>(postData, $"{ApiDomain}{apiUrl}");
                    if (result.IsSuccess)
                    {
                        Logger.Info($"Login success: ");
                        response.SetResult(result.Data);
                    }
                    else
                    {
                        response.SetError(result.Error);
                        Logger.Info($"FAIL to check Login: . ErrorCode: {result.Error?.ErrorCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"FAIL to check login: {ex.Message}");
                response.SetError(new ApiErrorResponse { Message = ex.Message });
            }
            finally
            {
                Logger.Debug($"END - Login.");
            }

            return response;
        }
    }
}
