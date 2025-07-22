using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ShopeeFood_WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected IConfiguration _configuration { get; } = null!;
        protected IHttpContextAccessor _httpContextAccessor { get; } = null!;
        protected readonly IMapper Mapper;

        public BaseController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            Mapper = mapper;
        }
    }
}
