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
}
