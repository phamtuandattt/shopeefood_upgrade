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
        public bool Success { get; set; } = false;

        public bool IsValidUser { get; set; } = false;
        public bool IsValidPwd { get; set; } = false;

        public CustomerResponseDto() { }

        public CustomerResponseDto(bool isValidUser, bool isValidPwd)
        {
            CustomerId = 0;
            FullName = "";
            Email = "";
            PhoneNumber = "";
            Avata = "";
            AccessToken = "";
            RefreshToken = "";
            RefreshTokenExpiryTime = new DateTime();
            Success = false;
            IsValidUser = isValidUser;
            IsValidPwd = isValidPwd;
        }
    }
}
