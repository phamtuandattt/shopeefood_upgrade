using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopeeFood.BLL.RequestDTOs.ShopRequestDTOs;
using ShopeeFood.BLL.ServicesContract.ShopServicesContract;
using ShopeeFood_WebApp.Models.Shops;

namespace ShopeeFood_WebApp.Controllers
{
    public class ShopController : BaseController
    {
        private readonly IShopServices _shopServices;

        public ShopController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper, IShopServices shopServices) 
            : base(configuration, httpContextAccessor, mapper)
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
                    ViewBag.ActiveCategoryId = shopRequestDto.FieldID;
                    ViewBag.PageTitle = "Shops";

                    var viewModel = Mapper.Map<List<ShopViewModel>>(shops.Data);

                    return View(viewModel);
                }
            }
            return NoContent();
        }
    }
}
