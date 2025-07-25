namespace ShopeeFood_WebAPI.ResponseDtos.CustomerResponseDto
{
    public class CustomerResponseDto
    {
        public int CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avata { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
