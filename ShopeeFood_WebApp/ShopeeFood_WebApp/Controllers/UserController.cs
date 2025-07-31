using AutoMapper;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopeeFood.BLL.RequestDTOs.CustomerRequestDto;
using ShopeeFood.BLL.ServicesContract.CustomerServicesContract;
using ShopeeFood.Infrastructure.Common.SessionManagement;
using ShopeeFood_WebApp.Models.Customers;
using System.Net.WebSockets;

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
            //var profile = await customerServices.GetCustomerProfile(HttpContext, "");
            //if (profile == null)
            //{
            //    HttpContext.Response.Redirect("/login");
            //}
            //var profileViewModel = Mapper.Map<CustomerProfileModel>(profile.Data);
            //return View(profileViewModel);

            return View();
        }

        [Route("/my-account")]
        public async Task<IActionResult> MyAccountModule()
        {
            var profile = await customerServices.GetCustomerProfile(HttpContext, "");
            if (profile == null || profile.Data == null)
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

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> LoginModule(string email, string password)
        {
            var requestDto = new CustomerLoginRequestDto(email, password);
            var clientSession = new ClientSession(HttpContext);
            var response = await customerServices.Login(HttpContext, requestDto);
            if (response is not null || response.Data != null)
            {
                var loginResutl = Mapper.Map<UserProfileModel>(response.Data);
                clientSession.AccessToken = loginResutl.AccessToken;
                clientSession.IsLogin = loginResutl.Success;
                clientSession.CurrentUser = loginResutl;

                return Redirect("/my-account");
            }

            return Redirect("/");
        }

        [HttpPost]
        [Route("/add-customer-address")]
        public async Task<IActionResult> AddCustomerAddress(CustomerAddressRequestDto requestDto)
        {
            var response = await customerServices.AddCustomerAddress(HttpContext, requestDto);
            if (response is not null && response.Data is not null)
            {
                return Json(response.Data);
            }

            return Redirect("/");
        }

        [HttpPost]
        [Route("/update-customer-address")]
        public async Task<IActionResult> UpdateCustomerAddress(CustomerAddressRequestDto requestDto)
        {
            var response = await customerServices.AddCustomerAddress(HttpContext, requestDto);
            if (response is not null && response.Data is not null)
            {
                return Json(response.Data);
            }

            return Redirect("/");
        }
    }
}
