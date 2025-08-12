using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopeeFood.Infrastructure.Common.Cache;

namespace ShopeeFood_WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected IConfiguration _configuration { get; } = null!;
        protected IHttpContextAccessor _httpContextAccessor { get; } = null!;
        protected readonly IMapper Mapper;

        protected readonly ICacheService _cacheService;

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

        public BaseController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper, ICacheService cacheService)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            Mapper = mapper;
            _cacheService = cacheService;
        }
    }
}
