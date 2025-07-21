namespace ShopeeFood_WebApp.Models.Cities
{
    public class CityViewModel
    {
        public List<CityModel> Cities { get; set; } = new List<CityModel>();

        public List<CityBusinessModel> CityBusinesses { get; set; } = new List<CityBusinessModel>();
    }

    public class CityModel 
    {
        public int CityId { get; set; }

        public string CityName { get; set; } = null!;
    }

    public class CityBusinessModel
    {
        public int FieldId { get; set; }
        public string? FieldName { get; set; }
    }

}
