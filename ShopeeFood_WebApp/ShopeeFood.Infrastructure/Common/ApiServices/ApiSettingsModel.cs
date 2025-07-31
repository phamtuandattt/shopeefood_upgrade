using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.Infrastructure.Common.ApiServices
{
    public class ApiSettingsModel
    {
        public string GetCities {  get; set; }
        public string GetBusinessOfCity { get; set; }
        public string GetShopOfCityFollowBusinessField { get; set; }
        public string GetShopCityBusinesses { get; set; }
        public string GetCustomerProfile { get; set; }
        public string UserLogin { get; set; }
        public string AddCustomerAddress { get; set; }
        public string UpdateCustomerAddress { get; set; }
        public string DeleteCustomerAddress { get; set; }

        public ApiSettingsModel()
        {
            GetCities = string.Empty;
            GetBusinessOfCity = string.Empty;
            GetShopOfCityFollowBusinessField = string.Empty;
            GetShopCityBusinesses = string.Empty;
            GetCustomerProfile = string.Empty;
            UserLogin = string.Empty;
            AddCustomerAddress = string.Empty;
            UpdateCustomerAddress = string.Empty;
            DeleteCustomerAddress = string.Empty;
        }
    }
}