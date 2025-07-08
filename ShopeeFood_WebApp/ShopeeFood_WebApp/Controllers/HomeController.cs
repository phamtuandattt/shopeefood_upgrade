using Microsoft.AspNetCore.Mvc;
using ShopeeFood.BLL.ServicesContract.BusinessServicesContract;
using ShopeeFood.Infrastructure.Logging;
using ShopeeFood_WebApp.Models;
using ShopeeFood_WebApp.Models.Businesses;
using ShopeeFood_WebApp.Models.Cities;
using System.Diagnostics;

namespace ShopeeFood_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IBusinessServices _businessServices;   

        public HomeController(IHttpContextAccessor httpContextAccessor, IBusinessServices businessServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _businessServices = businessServices;
        }

        public async Task<ActionResult> Index()
        {
            var data = await _businessServices.GetAllByCity(_httpContextAccessor.HttpContext, 1);

            var viewModel = new List<CityViewModel>();

            foreach(var city in data.Data)
            {
                viewModel.Add(new CityViewModel()
                {
                    CityId = city.CityId,
                    CityName = city.CityName,
                });
            }

            return View(viewModel);
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
