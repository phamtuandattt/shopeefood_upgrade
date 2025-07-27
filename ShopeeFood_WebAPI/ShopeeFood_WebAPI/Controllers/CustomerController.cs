
using ShopeeFood_WebAPI.BLL.Dtos.CustomerDtos;
using ShopeeFood_WebAPI.BLL.IServices;
using ShopeeFood_WebAPI.RequestModels.UserRequestDtos;
using ShopeeFood_WebAPI.ResponseDtos.CustomerResponseDto;
using System.Security.Claims;
using ShopeeFood_WebAPI.BLL.Servives;
using Microsoft.AspNetCore.Authorization;


namespace ShopeeFood_WebAPI.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public CustomerController(ICustomerServices customerServices, TokenService tokenService, IMapper mapper, IConfiguration configuration)
        {
            _customerServices = customerServices;
            _tokenService = tokenService;
            _mapper = mapper;
            _config = configuration;
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetCustomerProfile()
        {
            // Extract email from JWT token
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized(new ApiResponse
                {
                    status = HttpStatusCode.Unauthorized.ToString(),
                    message = ApiResponseMessage.INVALID_TOKEN,
                    data = ""
                });
            }
            
            var result = await _customerServices.GetCustomerWithDetailsByEmailAsync(email);
            if (result == null)
            {
                return NotFound(new ApiResponse
                {
                    status = HttpStatusCode.NotFound + "",
                    message = ApiResponseMessage.NOT_FOUND,
                    data = ""
                });
            }

            return Ok(new ApiResponse
            {
                status = HttpStatusCode.OK + "",
                message = ApiResponseMessage.SUCCESS,
                data = JsonConvert.SerializeObject(result)
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CustomerLoginRequestDto login)
        {
            var user = await _customerServices.GetCustomerByEmail(login.Email);
            //var result = hasher.VerifyHashedPassword(user, user.PasswordHash, login.Password); //if keep PasswordHasher<T>
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            {
                return Unauthorized(new ApiResponse
                {
                    status = HttpStatusCode.Unauthorized + "",
                    message = ApiResponseMessage.INVALID_TOKEN,
                    data = ""
                });
            }

            var jwtToken = _tokenService.GenerateToken(user);

            var cusResponseDto = new CustomerResponseDto()
            {
                CustomerId = user.CustomerId,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Avata = user.Avata,
                AccessToken = jwtToken,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            };

            return Ok(new ApiResponse
            {
                status = HttpStatusCode.Accepted + "",
                message = ApiResponseMessage.SUCCESS,
                data = JsonConvert.SerializeObject(cusResponseDto)
            });
        }

        //private string GenerateJwtToken(CustomerDto user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        //            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        //            new Claim("displayName", user.FullName ?? "")
        //        }),
        //        Expires = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpiresInMinutes"])),
        //        Issuer = _config["Jwt:Issuer"],
        //        Audience = _config["Jwt:Audience"],
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            var userExists = await _customerServices.Existed(request.Email);
            if (userExists)
            {
                return BadRequest(new ApiResponse
                {
                    status = HttpStatusCode.BadRequest + "",
                    message = ApiResponseMessage.IS_EXISTED,
                    data = ""
                });
            }    

            var customer = new CustomerDto
            {
                FullName = request.FullName,
                Email = request.Email,
                CreatedAt = DateTime.Now,
                Avata = request.Avata,

                // 🔐 Refresh token fields
                RefreshToken = _tokenService.GenerateRefreshToken(),
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7),
            };

            //var hasher = new PasswordHasher<CustomerDto>();
            //customer.PasswordHash = hasher.HashPassword(customer, request.Password);

            customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _customerServices.AddAsync(customer);

            var cusResponseDto = new CustomerResponseDto()
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Avata = customer.Avata,
                RefreshToken = customer.RefreshToken,
                RefreshTokenExpiryTime = customer.RefreshTokenExpiryTime
            };

            return Ok( new ApiResponse
            {
                status = HttpStatusCode.OK + "",
                message = ApiResponseMessage.REGISTERED_SUCCESS,
                data = JsonConvert.SerializeObject(cusResponseDto)
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.RefreshToken))
                return BadRequest("Invalid client request");

            //var principal = _tokenService.GetPrincipalFromExpiredToken(model.AccessToken);
            //var email = principal?.Identity?.Name;

            var user = await _customerServices.GetCustomerByEmail(model.Email);

            if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Unauthorized(new ApiResponse
                {
                    status = HttpStatusCode.NotAcceptable + "",
                    message = ApiResponseMessage.INVALID_REFRESH,
                    data = ""
                });
            }

            var newAccessToken = _tokenService.GenerateToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _customerServices.UpdateCustomer(user.CustomerId, user);

            var response = new CustomerResponseDto
            {
                CustomerId = user.CustomerId,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Avata = user.Avata,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            };

            return Ok(new ApiResponse
            {
                status = HttpStatusCode.OK + "",
                message = ApiResponseMessage.SUCCESS,
                data = JsonConvert.SerializeObject(response)
            });
        }
    }
}
