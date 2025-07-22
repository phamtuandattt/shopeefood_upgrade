using ShopeeFood.BLL.RequestDTOs.ShopRequestDTOs;
using ShopeeFood_WebApp.Models.Shops;
using ShopeeFood_WebApp.Models;
using ShopeeFood.BLL.ServicesContract.ShopServicesContract;

namespace ShopeeFood_WebApp.Services
{
    public class ModuleContentSerivces
    {
        private IConfiguration _configuration { get; } = null!;
        private IHttpContextAccessor _httpContextAccessor { get; } = null!;

        private readonly IShopServices _shopServices;


        public ModuleContentSerivces(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IShopServices shopServices)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            _shopServices = shopServices;
        }

        public async Task<List<ShopViewModel>> GetShopInHomePage()
        {
            var shops = await _shopServices.GetShopOfCityByBusinessField(new ShopRequestDto());
            var viewModel = new List<ShopViewModel>();
            if (shops.IsSuccess)
            {
                foreach (var shop in shops.Data.ToList())
                {
                    var sM = new ShopViewModel()
                    {
                        CityID = shop.CityID,
                        FieldID = shop.FieldID,
                        ShopID = shop.ShopID,
                        ShopName = shop.ShopName,
                        ShopImage = shop.ShopImage,
                        ShopAddress = shop.ShopAddress,
                        ShopUptime = shop.ShopUptime
                    };
                    viewModel.Add(sM);
                }
            }

            return viewModel;
        }
    }
}
