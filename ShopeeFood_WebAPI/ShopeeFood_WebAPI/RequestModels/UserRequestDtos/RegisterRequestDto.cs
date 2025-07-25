namespace ShopeeFood_WebAPI.RequestModels.UserRequestDtos
{
    public class RegisterRequestDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avata { get; set; }
    }
}
