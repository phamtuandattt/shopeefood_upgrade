using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopeeFood.BLL.ServicesContract.CustomerServicesContract;
using ShopeeFood.Infrastructure.Common.SessionManagement;
using ShopeeFood_WebApp.Models.Customers;

namespace ShopeeFood_WebApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly ICustomerServices customerServices;

        public UserController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper, ICustomerServices customerServices) 
            : base(configuration, httpContextAccessor, mapper)
        {
            this.customerServices = customerServices;
        }

        [Route("/profile")]
        public async Task<IActionResult> GetProfile()
        {
            var profile = await customerServices.GetCustomerProfile(HttpContext, "");
            if (profile == null)
            {
                HttpContext.Response.Redirect("/login");
            }
            var profileViewModel = Mapper.Map<CustomerProfileModel>(profile.Data);
            return View(profileViewModel);
        }

        [Route("/my-account")]
        public async Task<IActionResult> MyAccountModule()
        {
            var profile = await customerServices.GetCustomerProfile(HttpContext, "");
            if (profile == null)
            {
                HttpContext.Response.Redirect("/login");
            }
            var profileModel = Mapper.Map<CustomerProfileModel>(profile?.Data);

            var customerInfo = new CustomerInfoViewModel
            {
                CustomerId = profileModel.CustomerId,
                CustomerName = profileModel.FullName,
                Email = profileModel.Email,
                Avata = profileModel.Avata
            };

            var viewModel = new CustomerProfileViewModel()
            {
                CustomerInfo = customerInfo,
                CustomerAddresses = Mapper.Map<List<CustomerAddressViewModel>>(profileModel?.CustomerAddresses),
                CustomerExternalLogins = Mapper.Map<List<CustomerExternalLoginViewModel>>(profileModel?.CustomerExternalLogins)
            };

            ViewBag.PageTitle = "My Account";
            ViewBag.ActiveProfile = "my-account";
            
            return View(viewModel);
        }

        [Route("/my-favorite")]
        public ActionResult MyFavoriteModule()
        {
            ViewBag.PageTitle = "My Favorite";
            ViewBag.ActiveProfile = "my-favorite";
            return View();
        }

        [Route("/order-history")]
        public ActionResult OrderHistoryModule()
        {
            ViewBag.PageTitle = "Order History";
            ViewBag.ActiveProfile = "order-history";
            return View();
        }

        [Route("/login")]
        public ActionResult LoginModule()
        {
            

            return View();
        }
    }
}
