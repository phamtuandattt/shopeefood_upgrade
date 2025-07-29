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
            var profileViewModel = Mapper.Map<CustomerProfileViewModel>(profile);
            return View(profileViewModel);
        }

        [Route("/my-account")]
        public ActionResult MyAccountModule()
        {
            return View();
        }

        [Route("/my-favorite")]
        public ActionResult MyFavoriteModule()
        {
            return View();
        }

        [Route("/order-history")]
        public ActionResult OrderHistoryModule()
        {
            return View();
        }

        [Route("/login")]
        public ActionResult LoginModule()
        {
            

            return View();
        }
    }
}
