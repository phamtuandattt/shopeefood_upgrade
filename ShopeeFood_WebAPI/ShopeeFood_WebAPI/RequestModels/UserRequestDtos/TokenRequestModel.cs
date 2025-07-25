namespace ShopeeFood_WebAPI.RequestModels.UserRequestDtos
{
    public class TokenRequestModel
    {
        public string? RefreshToken { get; set; }
        public string? Email { get; set; }
    }
}
