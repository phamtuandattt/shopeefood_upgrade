using Microsoft.AspNetCore.Mvc;

namespace ShopeeFood_WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected IConfiguration _configuration { get; } = null!;
        protected IHttpContextAccessor _httpContextAccessor { get; } = null!;

        public BaseController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
