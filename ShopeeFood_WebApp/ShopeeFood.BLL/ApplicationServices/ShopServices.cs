using log4net.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopeeFood.BLL.DTOS.CityDTOs;
using ShopeeFood.BLL.DTOS.ShopDTOs;
using ShopeeFood.BLL.RequestDTOs.ShopRequestDTOs;
using ShopeeFood.BLL.ServicesContract.ShopServicesContract;
using ShopeeFood.Infrastructure.Common.ApiServices;
using ShopeeFood.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.ApplicationServices
{
    public class ShopServices : BaseApiServices, IShopServices
    {
        private readonly IConfiguration _configuration;
        //private string ApiDomain;

        public ShopServices(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base (httpContextAccessor, configuration)
        {
            _configuration = configuration;
            //ApiDomain = _configuration["ApiDomain"];
        }

        public async Task<AppActionResult<IEnumerable<ShopResponseDtos>, ApiErrorResponse>> GetShopOfCityByBusinessField(ShopRequestDto request)
        {
            Logger.Info("BEGIN - Get shop of the city by business");
            var response = new AppActionResult<IEnumerable<ShopResponseDtos>, ApiErrorResponse>();
            //var apiUrl = _configuration["GetShopOfCityFollowBusinessField"];
            var apiSetting = ApiSettingServices.LoadApiSettings(_httpContextAccessor.HttpContext);
            var apiUrl = apiSetting.GetShopOfCityFollowBusinessField;
            try
            {
                if (request != null)
                {
                    var postData = SerializeParams(request);

                    var result = await RestServices.PostAsync<IEnumerable<ShopResponseDtos>, ApiErrorResponse>(postData, $"{ApiDomain}{apiUrl}");
                    if (result.IsSuccess)
                    {
                        Logger.Info($"Get shop of the city by business: ");
                        response.SetResult(result.Data);
                    }
                    else
                    {
                        response.SetError(result.Error);
                        Logger.Info($"FAIL to Get shop of the city by business . ErrorCode: {result.Error?.ErrorCode}");
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Info($"FAIL to get shop of the city by business: {ex.Message}");
                response.SetError(new ApiErrorResponse { Message = ex.Message });
            }
            finally
            {
                Logger.Debug($"END - GetShopOfCityByBusinessField.");
            }

            return response;
        }
    }
}
