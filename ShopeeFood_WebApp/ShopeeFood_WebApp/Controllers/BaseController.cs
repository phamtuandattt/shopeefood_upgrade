using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopeeFood.BLL.DTOS.PageSettingDTOs;
using ShopeeFood.BLL.ServicesContract.PageSettingServicesContract;
using ShopeeFood.Infrastructure.Common;
using ShopeeFood.Infrastructure.Common.Cache;

namespace ShopeeFood_WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected IConfiguration _configuration { get; } = null!;
        protected IHttpContextAccessor _httpContextAccessor { get; } = null!;
        protected readonly IMapper Mapper;

        protected readonly ICacheService _cacheService;
        protected readonly IPageSettingServices _pageSettingServices;

        protected string AppName
        {
            get => _configuration["InstanceName"];
        }


        public BaseController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            Mapper = mapper;
        }

        public BaseController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper, ICacheService cacheService, IPageSettingServices pageSettingServices)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            Mapper = mapper;
            _cacheService = cacheService;
            _pageSettingServices = pageSettingServices;
        }

        public async Task<PageSettingDto> GetPageSettingDto(HttpContext httpContext, string pagePath)
        {
            try
            {
                var pageSettings = await _pageSettingServices.GetPageSettings(httpContext);
                var lstPage = pageSettings.Data;
                if (lstPage != null)
                {
                    var rs = lstPage.FirstOrDefault(p => p.PagePath == pagePath);
                    if (rs != null)
                    {
                        return rs;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            var pageName = Helper.SentenceCase(pagePath.Replace('-', ' '));
            return new PageSettingDto()
            {
                PageName = pageName,
                PagePath = pagePath,
            };
        }
    }
}
