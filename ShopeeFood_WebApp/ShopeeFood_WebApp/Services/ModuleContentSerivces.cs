using ShopeeFood.BLL.RequestDTOs.ShopRequestDTOs;
using ShopeeFood_WebApp.Models.Shops;
using ShopeeFood_WebApp.Models;
using ShopeeFood.BLL.ServicesContract.ShopServicesContract;
using AutoMapper;
using log4net.Util;
using ShopeeFood_WebApp.Models.Banners;

namespace ShopeeFood_WebApp.Services
{
    public class ModuleContentSerivces
    {
        private IConfiguration _configuration { get; } = null!;
        private IHttpContextAccessor _httpContextAccessor { get; } = null!;

        private readonly IShopServices _shopServices;
        private readonly IMapper _mapper;


        public ModuleContentSerivces(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IShopServices shopServices, IMapper mapper)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            _shopServices = shopServices;
            _mapper = mapper;
        }

        public async Task<List<ShopViewModel>> GetContentBannerRight()
        {
            var shops = await _shopServices.GetShopOfCityByBusinessField(new ShopRequestDto());
            if (shops.IsSuccess)
            {
                var viewModel = _mapper.Map<List<ShopViewModel>>(shops.Data);
                return viewModel;
            }
            return new List<ShopViewModel>();
        }

        public async Task<BannerLeftViewModel> GetContentBannerLeft()
        {
            var viewModel = new BannerLeftViewModel()
            {
                TitleBanner = "Đặt Đồ ăn, giao hàng từ 20'...",
                Local = "Có 87917 địa điểm ở TP. HCM từ 00:00 - 23:59",
                Categories = new List<string>()
                {
                    "Đồ ăn", "Tráng miệng", "Đồ chay", "Đồ uống", "Bánh kem", "Pizza/Buger", "Món lẩu", "Shushi", "Mì", "Phờ", "Cơm hộp"
                }
            };

            return viewModel;
        }
    }
}
