namespace ShopeeFood_WebApp.Models.Shops
{
    public class ShopViewModel
    {
        public int CityID { get; set; }
        public int FieldID { get; set; }
        public int ShopID { get; set; }
        public string ShopName { get; set; } = string.Empty;
        public string ShopImage { get; set; } = string.Empty;
        public string ShopAddress {  get; set; } = string.Empty;
        public string ShopUptime { get; set; } = string.Empty;
        public int TotalRecords { get; set; }
    }
}
