using Newtonsoft.Json;
using ShopeeFood.BLL.DTOS.CustomerDTOs;
using ShopeeFood_WebApp.Models.Customers;

namespace ShopeeFood_WebApp.Models
{
    public class JsonResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object DataReturn { get; set; }
    }

    public class ShopCityBussinessJsonResponse
    {
        public object Businesses { get; set; }
        public object Shops { get; set; }
    }

    public class PopupMessageContentJsonResponse
    {
        [JsonProperty("success")]
        public bool success { get; set; } = false;

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("isAutoClose")]
        public bool isAutoClose { get; set; }

        [JsonProperty("dataReturn")]
        public object dataReturn { get; set; }
    }
}
