using Microsoft.AspNetCore.Mvc;
using ShopeeFood.BLL.ServicesContract.BusinessServicesContract;
using ShopeeFood.Infrastructure.Logging;
using ShopeeFood_WebApp.Models;
using ShopeeFood_WebApp.Models.Businesses;
using ShopeeFood_WebApp.Models.Cities;
using System.Diagnostics;

namespace ShopeeFood_WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBusinessServices _businessServices;   

        public HomeController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBusinessServices businessServices)
            : base(configuration, httpContextAccessor)
        {
            _businessServices = businessServices;
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
