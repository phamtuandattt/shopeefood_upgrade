using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopeeFood.BLL.DTOS.BusinessDTOs;
using ShopeeFood.BLL.DTOS.CityDTOs;
using ShopeeFood.BLL.ServicesContract.BusinessServicesContract;
using ShopeeFood.Infrastructure.Common.ApiServices;
using ShopeeFood.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.ApplicationServices
{
    public class BusinessServices : IBusinessServices
    {
        private RestServices RestServices { get; set; }
        private IHttpContextAccessor _httpContextAccessor { get; set; }
        private readonly IConfiguration _configuration;
        private string ApiDomain;

        public BusinessServices(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            RestServices = _httpContextAccessor.HttpContext.RequestServices.GetService<RestServices>();
            ApiDomain = _configuration["ApiDomain"];
        }

        public async Task<AppActionResult<IEnumerable<CityDto>, ApiErrorResponse>> GetAllByCity(HttpContext httpContext, int cityId)
        {
            Logger.Info("BEGIN - Get business of the city");
            var response = new AppActionResult<IEnumerable<CityDto>, ApiErrorResponse>();
            var apiUrl = _configuration["GetCities"];
            try
            {
                if (cityId != 0)
                {
                    var result = await RestServices.GetAsync<IEnumerable<CityDto>, ApiErrorResponse>(null, $"{ApiDomain}{apiUrl}");
                    if (result.IsSuccess)
                    {
                        Logger.Info($"Get list of the business: ");
                        response.SetResult(result.Data);
                    }
                    else
                    {
                        response.SetError(result.Error);
                        Logger.Info($"FAIL to getlist of the business: . ErrorCode: {result.Error?.ErrorCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"FAIL to getlist of the business: {ex.Message}");
                response.SetError(new ApiErrorResponse { Message = ex.Message });
            }
            finally
            {
                Logger.Debug($"END - GetAccountAsync.");
            }

            return response;
        }
    }
}
