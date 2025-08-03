namespace ShopeeFood_WebAPI.RequestModels.UserRequestDtos
{
    public class ResetPasswordRequestDto
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }

    // Request reset
    public class ForgotPasswordRequestDto
    {
        public string Email { get; set; }
    }
}