using AutoMapper;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopeeFood.BLL.RequestDTOs.CustomerRequestDto;
using ShopeeFood.BLL.ServicesContract.CustomerServicesContract;
using ShopeeFood.Infrastructure.Common;
using ShopeeFood.Infrastructure.Common.SessionManagement;
using ShopeeFood_WebApp.Models;
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

        [HttpGet]
        [Route("/login")]
        public ActionResult LoginModule()
        {
            ViewBag.PageTitle = "Login";
            return View();
        }

        [HttpGet]
        [Route("/reset-password")]
        public ActionResult ResetPasswordModule(string token)
        {
            var clientSession = new ClientSession(HttpContext);
            clientSession.TokenResetpassword = token;
            ViewBag.PageTitle = "Reset Password";
            return View();
        }

        [HttpPost]
        [Route("/reset-password")]
        public async Task<IActionResult> ResetPasswordModule(string token, string password)
        {
            var clientSession = new ClientSession(HttpContext);
            var objReturn = new PopupMessageContentJsonResponse();
            var requestDto = new ResetpasswordRequestDto() { Token = clientSession.TokenResetpassword, NewPassword = password };
            var response = await customerServices.ResetPassword(HttpContext, requestDto);
            if (response is not null && response.Data is not null)
            {
                if (response.Data.IsSuccess)
                {
                    objReturn.success = response.Data.IsSuccess;
                    //return Redirect("/my-account");
                    return Json(objReturn);
                }
            }
            objReturn.success = response.Data.IsSuccess;
            objReturn.type = PopupManagement.GetPopupType(PopupType.Error);
            objReturn.title = PopupManagement.GetTitlePopup(PopupAction.Reset, false);
            objReturn.message = response.Data.Message;

            return Json(objReturn);
        }

        [HttpPost]
        [Route("/membership-login")]
        public async Task<IActionResult> MembershipLogin(CustomerLoginRequestDto requestDto)
        {
            var clientSession = new ClientSession(HttpContext);
            var objReturn = new LoginStatusJsonResponse();
            var response = await customerServices.Login(HttpContext, requestDto);
            if (response is not null || response.Data != null)
            {
                var loginResutl = Mapper.Map<UserProfileModel>(response.Data);
                if (response.Data.IsValidUser && response.Data.IsValidPwd)
                {
                    clientSession.AccessToken = loginResutl.AccessToken;
                    clientSession.IsLogin = loginResutl.Success;
                    clientSession.CurrentUser = loginResutl;
                    objReturn.isRedirect = true;
                    //return Redirect("/my-account");
                    return Json(objReturn);
                }

                if (!response.Data.IsValidUser) // User not existed
                {
                    objReturn.isValidUser = loginResutl.IsValidUser;
                    objReturn.message = ErrorMessage.GetValue(ErrorCode.InvalidEmail);

                    return Json(objReturn);
                }

                if (!response.Data.IsValidPwd) // Password incorrect
                {
                    objReturn.isValidPwd = loginResutl.IsValidPwd;
                    objReturn.message = ErrorMessage.GetValue(ErrorCode.PasswordDoesNotMatch);

                    return Json(objReturn);
                }
            }

            objReturn.isValidUser = true;
            objReturn.message = ErrorMessage.GetValue(ErrorCode.InvalidEmail);

            return Json(objReturn);
        }

        [HttpPost]
        [Route("/add-customer-address")]
        public async Task<IActionResult> AddCustomerAddress(CustomerAddressRequestDto requestDto)
        {
            var objReturn = new PopupMessageContentJsonResponse();
            var response = await customerServices.AddCustomerAddress(HttpContext, requestDto);
            if (response is not null && response.Data is not null)
            {
                objReturn.success = true;
                objReturn.type = PopupManagement.GetPopupType(PopupType.Success);
                objReturn.title = PopupManagement.GetTitlePopup(PopupAction.Add, true);
                objReturn.message = PopupManagement.ADD_CUSTOMER_ADDRESS_SUCCESS_MESSAGE;
                objReturn.dataReturn = response.Data;
                return Json(objReturn);
            }
            objReturn.success = false;
            objReturn.type = PopupManagement.GetPopupType(PopupType.Success);
            objReturn.title = PopupManagement.GetTitlePopup(PopupAction.Add, true);
            objReturn.message = PopupManagement.ADD_CUSTOMER_ADDRESS_SUCCESS_MESSAGE;
            objReturn.dataReturn = response.Data;
            return Json(objReturn);
        }

        [HttpPost]
        [Route("/update-customer-address")]
        public async Task<IActionResult> UpdateCustomerAddress(CustomerAddressRequestDto requestDto)
        {
            var objReturn = new PopupMessageContentJsonResponse();
            var response = await customerServices.UpdateCustomerAddress(HttpContext, requestDto);
            if (response is not null && response.Data is not null)
            {
                objReturn.success = true;
                objReturn.type = PopupManagement.GetPopupType(PopupType.Success);
                objReturn.title = PopupManagement.GetTitlePopup(PopupAction.Update, true);
                objReturn.message = PopupManagement.UPDATE_CUSTOMER_ADDRESS_SUCCESS_MESSAGE;
                objReturn.dataReturn = response.Data;
                return Json(objReturn);
            }

            objReturn.success = false;
            objReturn.type = PopupManagement.GetPopupType(PopupType.Error);
            objReturn.title = PopupManagement.GetTitlePopup(PopupAction.Update, true);
            objReturn.message = PopupManagement.UPDATE_CUSTOMER_ADDRESS_FAIL_MESSAGE;
            objReturn.dataReturn = null;
            return Json(objReturn);
        }

        [HttpPost]
        [Route("/delete-customer-address")]
        public async Task<IActionResult> DeleteCustomerAddress(int customerAddressId)
        {
            var objReturn = new PopupMessageContentJsonResponse();
            var requestDto = new CustomerAddressRequestDto() { AddressId = customerAddressId };
            var response = await customerServices.DeleteCustomerAddress(HttpContext, requestDto);
            if (response is not null && response.Data is not null)
            {
                if (response.Data.IsSuccess)
                {
                    objReturn.success = true;
                    objReturn.type = PopupManagement.GetPopupType(PopupType.Success);
                    objReturn.title = PopupManagement.GetTitlePopup(PopupAction.Delete, true);
                    objReturn.message = PopupManagement.DELETE_SUCCESS_MESSAGE;

                    return Json(objReturn);
                }
            }
            objReturn.success = false;
            objReturn.type = PopupManagement.GetPopupType(PopupType.Error);
            objReturn.title = PopupManagement.GetTitlePopup(PopupAction.Delete, false);
            objReturn.message = PopupManagement.DELETE_FAIL_MESSAGE;

            return Json(objReturn);
        }
    }
}
