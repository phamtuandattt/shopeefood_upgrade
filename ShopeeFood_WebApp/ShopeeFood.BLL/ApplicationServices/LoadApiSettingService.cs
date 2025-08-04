using Microsoft.AspNetCore.Http;
using ShopeeFood.Infrastructure.Common.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.ApplicationServices
{
    public class LoadApiSettingService
    {
        public ApiSettingsModel LoadApiSettings(HttpContext httpContext)
        {
            var settings = new ApiSettingsModel()
            {
                GetCities = "/api/cities",
                GetBusinessOfCity = "/api/cities/{0}",
                GetShopOfCityFollowBusinessField = "/api/cities/shops",
                GetShopCityBusinesses = "/api/cities/business-shops",
                GetCustomerProfile = "/api/customers/profile",
                UserLogin = "/api/customers/login",
                AddCustomerAddress = "/api/customers/add-customer-address",
                UpdateCustomerAddress = "/api/customers/update-address",
                DeleteCustomerAddress = "/api/customers/delete-address",
                Resetpassword = "/api/customers/reset-password"
            };
            return settings;
        }
    }
}
