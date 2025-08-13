using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopeeFood.BLL.DTOS.CustomerDTOs;
using ShopeeFood.BLL.DTOS.PageSettingDTOs;
using ShopeeFood.BLL.ServicesContract.PageSettingServicesContract;
using ShopeeFood.Infrastructure.Common.ApiServices;
using ShopeeFood.Infrastructure.Common.SessionManagement;
using ShopeeFood.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.ApplicationServices.PageSettingServices
{
    public class PageSettingServices : BaseApiServices, IPageSettingServices
    {
        public PageSettingServices(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) 
            : base(httpContextAccessor, configuration)
        {
        }

        public async Task<AppActionResult<List<PageSettingDto>, ApiErrorResponse>> GetPageSettings(HttpContext httpContext)
        {
            Logger.Info("BENGIN - Get page setting");
            var response = new AppActionResult<List<PageSettingDto>, ApiErrorResponse>();
            var clientSession = new ClientSession(_httpContextAccessor);
            var apiSetting = ApiSettingServices.LoadApiSettings(httpContext);
            var apiUrl = apiSetting.PageSetting;
            try
            {
                var result = await RestServices.GetAsync<List<PageSettingDto>, ApiErrorResponse>(null, $"{ApiDomain}{apiUrl}");
                if (result.IsSuccess)
                {
                    Logger.Info($"Get page setting request success! ");
                    response.SetResult(result.Data);
                }
                else
                {
                    response.SetError(result.Error);
                    Logger.Info($"FAIL to get page setting request: . ErrorCode: {result.Error?.ErrorCode}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"FAIL to get page setting request: {ex.Message}");
                response.SetError(new ApiErrorResponse { Message = ex.Message });
            }
            finally
            {
                Logger.Debug($"END - Get page setting request.");
            }

            return response;
        }
    }
}
