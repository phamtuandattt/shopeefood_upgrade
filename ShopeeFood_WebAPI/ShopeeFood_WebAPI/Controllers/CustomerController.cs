
using ShopeeFood_WebAPI.BLL.Dtos.CustomerDtos;
using ShopeeFood_WebAPI.BLL.IServices;
using ShopeeFood_WebAPI.RequestModels.UserRequestDtos;
using ShopeeFood_WebAPI.ResponseDtos.CustomerResponseDto;
using System.Security.Claims;
using ShopeeFood_WebAPI.BLL.Servives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net.WebSockets;
using Microsoft.EntityFrameworkCore;
using ShopeeFood_WebAPI.Infrastructure.Common.Email;
using ShopeeFood_WebAPI.DAL.Models;


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
        private readonly IEmailServices _emailServices;

        public CustomerController(ICustomerServices customerServices, TokenService tokenService, IMapper mapper, IConfiguration configuration, IEmailServices emailServices)
        {
            _customerServices = customerServices;
            _tokenService = tokenService;
            _mapper = mapper;
            _config = configuration;
            _emailServices = emailServices;
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
            //if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            if (user == null)
            {
                return Ok(new ApiResponse
                {
                    status = HttpStatusCode.Unauthorized + "",
                    message = ApiResponseMessage.INVALID_TOKEN,
                    data = JsonConvert.SerializeObject(new CustomerResponseDto(false, false))
                });
            }

            var hasher = new PasswordHasher<CustomerDto>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, login.Password); //if keep PasswordHasher<T>
            if (result == PasswordVerificationResult.Failed)
            {
                return Ok(new ApiResponse
                {
                    status = HttpStatusCode.NotFound + "",
                    message = ApiResponseMessage.NOT_FOUND,
                    data = JsonConvert.SerializeObject(new CustomerResponseDto(true, false))
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
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                Success = true,

                IsValidUser = true,
                IsValidPwd = true,
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

            var hasher = new PasswordHasher<CustomerDto>();
            customer.PasswordHash = hasher.HashPassword(customer, request.Password);

            //customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _customerServices.AddAsync(customer);

            var cusResponseDto = new CustomerResponseDto()
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Avata = customer.Avata,
                RefreshToken = customer.RefreshToken,
                RefreshTokenExpiryTime = customer.RefreshTokenExpiryTime,
                Success = true
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
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
                Success = true
            };

            return Ok(new ApiResponse
            {
                status = HttpStatusCode.OK + "",
                message = ApiResponseMessage.SUCCESS,
                data = JsonConvert.SerializeObject(response)
            });
        }

        [Authorize]
        [HttpPost("add-customer-address")]
        public async Task<IActionResult> AddCustomerAddress([FromBody] CustomerAddressRequestDto requestDto)
        {
            // Extract email from JWT token
            var IsParseSucces = int.TryParse((User.FindFirst(ClaimTypes.NameIdentifier)?.Value), out var customerId);

            if (!IsParseSucces || requestDto is null)
            {
                return Unauthorized(new ApiResponse
                {
                    status = HttpStatusCode.Unauthorized.ToString(),
                    message = ApiResponseMessage.INVALID_TOKEN,
                    data = ""
                });
            }

            try
            {
                var addModel = _mapper.Map<CustomerAddressDto>(requestDto);
                addModel.CustomerId = customerId;

                var result = await _customerServices.AddCustomerAddressAsync(addModel);
                if (result != null)
                {
                    return Ok(new ApiResponse
                    {
                        status = HttpStatusCode.OK + "",
                        message = ApiResponseMessage.SUCCESS,
                        data = JsonConvert.SerializeObject(result)
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    status = HttpStatusCode.InternalServerError + "",
                    message = ex.Message,
                    data = null
                }); ;
            }
            return BadRequest(new ApiResponse
            {
                status = HttpStatusCode.InternalServerError + "",
                message = ApiResponseMessage.BAD_REQUEST,
                data = null
            }); ;
        }


        [Authorize]
        [HttpPost("update-address")]
        public async Task<IActionResult> UpdateCustomerAddress([FromBody] CustomerAddressRequestDto requestDto)
        {
            // Extract email from JWT token
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            //var email = "dattest_api@yopmail.com";

            if (string.IsNullOrEmpty(email) || requestDto is null)
            {
                return Unauthorized(new ApiResponse
                {
                    status = HttpStatusCode.Unauthorized.ToString(),
                    message = ApiResponseMessage.INVALID_TOKEN,
                    data = ""
                });
            }

            var result = await _customerServices.GetCustomerAddressByEmail(email);
            if (result == null)
            {
                return NotFound(new ApiResponse
                {
                    status = HttpStatusCode.NotFound + "",
                    message = ApiResponseMessage.NOT_FOUND,
                    data = ""
                });
            }
            
            var updateModel = result.Find(adr => adr.AddressId == requestDto.AddressId);
            if (updateModel != null)
            {
                var updateModelNew = _mapper.Map(requestDto, updateModel);

                var response = await _customerServices.UpdateCustomerAddress(updateModelNew);
                if (response is not null)
                {
                    return Ok(new ApiResponse
                    {
                        status = HttpStatusCode.OK + "",
                        message = ApiResponseMessage.SUCCESS,
                        data = JsonConvert.SerializeObject(response)
                    });
                }
            }

            return BadRequest(new ApiResponse
            {
                status = HttpStatusCode.BadRequest.ToString(),
                message = ApiResponseMessage.NOT_FOUND,
                data = null
            });
        }

        [Authorize]
        [HttpPost("delete-address")]
        public async Task<IActionResult> DeleteCustomerAddress([FromBody] CustomerAddressRequestDto requestDto)
        {
            // Extract email from JWT token
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            //var email = "dattest_api@yopmail.com";

            if (string.IsNullOrEmpty(email) || requestDto is null)
            {
                return Unauthorized(new ApiResponse
                {
                    status = HttpStatusCode.Unauthorized.ToString(),
                    message = ApiResponseMessage.INVALID_TOKEN,
                    data = ""
                });
            }
            try
            {
                await _customerServices.DeleteAddressAsync(email, requestDto.AddressId);
                return Ok(new ApiResponse
                {
                    status = HttpStatusCode.OK.ToString(),
                    message = ApiResponseMessage.SUCCESS,
                    data = JsonConvert.SerializeObject(new ApiModelResponse(true, ApiResponseMessage.SUCCESS))
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    status = HttpStatusCode.InternalServerError.ToString(),
                    message = ex.Message,
                    data = ""
                });
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto request)
        {
            //var user = await _context.Customers.FirstOrDefaultAsync(c => c.Email == request.Email);

            var user = await _customerServices.GetCustomerByEmail(request.Email);
            if (user == null)
            {
                return Ok(new ApiResponse
                {
                    status = HttpStatusCode.NotFound.ToString(),
                    message = ApiResponseMessage.NOT_FOUND,
                    data = JsonConvert.SerializeObject(new ApiModelResponse(false, ApiResponseMessage.NOT_FOUND))
                });

            }    

            var token = Guid.NewGuid().ToString();
            user.ResetToken = token;
            user.ResetTokenExpiryTime = DateTime.UtcNow.AddHours(1);

            await _customerServices.UpdateCustomer(user.CustomerId, user);

            // send email
            var resetLink = $"https://yourwebapp.com/reset-password?token={token}";
            await _emailServices.SendEmailAsync(user.Email, 
                "Reset Your Password", 
                $"<p>Hello,</p><p>Click <a href='{resetLink}'>here</a> to reset your password. This link is valid for 1 hour.</p>");

            //return Ok(new { message = "Reset link has been sent to your email." });
            return Ok(new ApiResponse
            {
                status = HttpStatusCode.OK.ToString(),
                message = ApiResponseMessage.RESET_LINK,
                data = JsonConvert.SerializeObject(new ApiModelResponse(true, ApiResponseMessage.RESET_LINK))
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
        {
            var user = await _customerServices.GetCustomerByResetToken(request.Token);
            if (user == null)
            {
                return Ok(new ApiResponse
                {
                    status = HttpStatusCode.OK.ToString(),
                    message = ApiResponseMessage.INVALID_TOKEN_RESET,
                    data = JsonConvert.SerializeObject(new ApiModelResponse(false, ApiResponseMessage.INVALID_TOKEN_RESET))
                });
            }

            var hasher = new PasswordHasher<CustomerDto>();
            user.PasswordHash = hasher.HashPassword(user, request.NewPassword); // or your hash method
            user.ResetToken = null;
            user.ResetTokenExpiryTime = null;

            await _customerServices.UpdateCustomer(user.CustomerId, user);

            return Ok(new ApiResponse
            {
                status = HttpStatusCode.OK.ToString(),
                message = ApiResponseMessage.RESET_SUCCESS,
                data = JsonConvert.SerializeObject(new ApiModelResponse(true, ApiResponseMessage.RESET_SUCCESS))
            });
        }
    }
}
