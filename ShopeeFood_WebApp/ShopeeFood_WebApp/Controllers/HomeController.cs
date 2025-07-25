using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopeeFood.BLL.RequestDTOs.ShopRequestDTOs;
using ShopeeFood.BLL.ServicesContract.BusinessServicesContract;
using ShopeeFood.BLL.ServicesContract.ShopServicesContract;
using ShopeeFood.Infrastructure.Logging;
using ShopeeFood_WebApp.Models;
using ShopeeFood_WebApp.Models.Businesses;
using ShopeeFood_WebApp.Models.Cities;
using ShopeeFood_WebApp.Models.Shops;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;

namespace ShopeeFood_WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBusinessServices _businessServices;
        private readonly IShopServices _shopServices;

        public HomeController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IMapper mapper, IBusinessServices businessServices, IShopServices shopServices)
            : base(configuration, httpContextAccessor, mapper)
        {
            _businessServices = businessServices;
            _shopServices = shopServices;
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


        [HttpPost]
        [Route("/home/shops")]
        public async Task<ActionResult> GetShopInHomePage([FromForm] ShopRequestDto shopRequestDto)
        {
            var objReturn = new JsonResponse();
            if (shopRequestDto != null)
            {
                var shops = await _shopServices.GetShopOfCityByBusinessField(shopRequestDto);
                if (shops.IsSuccess)
                {
                    objReturn.DataReturn = Mapper.Map<List<ShopViewModel>>(shops.Data);
                    ViewBag.ActiveCategoryId = shopRequestDto.FieldID;
                    ViewBag.PageTitle = "Shops";
                }
            }

            return Json(objReturn);
        }

        [HttpPost]
        [Route("/cites/business-field")]
        public async Task<ActionResult> GetBusinessField(int regionId)
        {
            var objReturn = new ShopCityBussinessJsonResponse();

            if (regionId != 0)
            {
                var requestDto = new ShopRequestDto()
                {
                    CityID = regionId,
                    FieldID = 1,
                    PageNumber = 1,
                    PageSize = 6
                };

                var data = await _businessServices.GetShopCityBusinesses(_httpContextAccessor.HttpContext, requestDto);
                if (data.IsSuccess)
                {
                    var response = Mapper.Map<ShopCityBussinessJsonResponse>(data.Data);
                    objReturn.Businesses = response.Businesses;
                    objReturn.Shops = response.Shops;
                }
            }

            return Json(objReturn);
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
