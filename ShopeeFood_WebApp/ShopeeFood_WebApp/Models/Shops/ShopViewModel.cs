namespace ShopeeFood_WebApp.Models.Shops
{
    public class ShopViewModel
    {
        public int CityID { get; set; }
        public int FieldID { get; set; }
        public int ShopID { get; set; }
        public string ShopName { get; set; } = string.Empty;
        public int TotalRecords { get; set; }
    }
}
