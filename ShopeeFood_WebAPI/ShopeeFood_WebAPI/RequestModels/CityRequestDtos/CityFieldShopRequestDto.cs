namespace ShopeeFood_WebAPI.RequestModels.CityRequestDtos
{
    public class CityFieldShopRequestDto
    {
        public int CityID { get; set; } = 1;
        public int FieldID { get; set; } = 1;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}
