using Microsoft.AspNetCore.Mvc;
using ShopeeFood.BLL.RequestDTOs.ShopRequestDTOs;
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
            ViewBag.PageTitle = "Home";
            return View();
        }

        [HttpGet]
        [Route("/Home/GetMenuData")]
        public IActionResult GetMenuData(int id)
        {
            // Simulate some data (You can query database here)
            var allData = new Dictionary<int, List<object>> {
            { 1, new List<object> {
                new { name = "Res A", description = "Nice food" },
                new { name = "Res B", description = "Spicy food" }
            }},
            { 2, new List<object> {
                new { name = "Res C", description = "Hot food" }
            }},
                // add more mock or real data per ID
        };

            ViewBag.ActiveCategoryId = id;

            var result = allData.ContainsKey(id) ? allData[id] : new List<object>();
            return Json(result);
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
