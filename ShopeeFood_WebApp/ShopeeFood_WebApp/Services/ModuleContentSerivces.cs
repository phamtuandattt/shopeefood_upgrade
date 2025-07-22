using ShopeeFood.BLL.RequestDTOs.ShopRequestDTOs;
using ShopeeFood_WebApp.Models.Shops;
using ShopeeFood_WebApp.Models;
using ShopeeFood.BLL.ServicesContract.ShopServicesContract;
using AutoMapper;
using log4net.Util;

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

        public async Task<List<ShopViewModel>> GetShopInHomePage()
        {
            var shops = await _shopServices.GetShopOfCityByBusinessField(new ShopRequestDto());
            if (shops.IsSuccess)
            {
                var viewModel = _mapper.Map<List<ShopViewModel>>(shops.Data);
                return viewModel;
            }
            return new List<ShopViewModel>();
        }
    }
}
