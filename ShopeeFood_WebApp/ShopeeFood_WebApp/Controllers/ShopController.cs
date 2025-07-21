using Microsoft.AspNetCore.Mvc;
using ShopeeFood.BLL.RequestDTOs.ShopRequestDTOs;
using ShopeeFood.BLL.ServicesContract.ShopServicesContract;
using ShopeeFood_WebApp.Models.Shops;

namespace ShopeeFood_WebApp.Controllers
{
    public class ShopController : BaseController
    {
        private readonly IShopServices _shopServices;

        public ShopController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IShopServices shopServices) : base(configuration, httpContextAccessor)
        {
            _shopServices = shopServices;
        }

        [Route("shops")]
        [Route("/shops/category")]
        public async Task<ActionResult> Shops(int cityId, int fieldId, int pageSize, int pageNumber)
        {
            var shopRequestDto = new ShopRequestDto()
            {
                CityID = cityId,
                FieldID = fieldId,
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            if (shopRequestDto != null)
            {
                var shops = await _shopServices.GetShopOfCityByBusinessField(shopRequestDto);
                if (shops.IsSuccess)
                {
                    var lstS = new List<ShopViewModel>();
                    foreach (var shop in shops.Data.ToList())
                    {
                        var viewModel = new ShopViewModel()
                        {
                            CityID = shop.CityID,
                            FieldID = shop.FieldID,
                            ShopID = shop.ShopID,
                            ShopName = shop.ShopName,
                        };
                        lstS.Add(viewModel);
                    }

                    ViewBag.ActiveCategoryId = shopRequestDto.FieldID;
                    ViewBag.PageTitle = "Shops";

                    return View(lstS);
                }
            }
            return NoContent();
        }
    }
}
