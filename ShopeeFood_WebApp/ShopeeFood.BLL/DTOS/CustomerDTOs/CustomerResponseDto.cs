using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.DTOS.CustomerDTOs
{
    public class CustomerResponseDto
    {
        public int CustomerId { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
        public string? PasswordHash { get; set; }

        public string? Avata { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public List<CustomerAddressDto>? CustomerAddresses { get; set; }

        public List<CustomerExternalLoginDto>? CustomerExternalLogins { get; set; }
    }

    public class CustomerAddressDto
    {
        public int AddressId { get; set; }

        public int? CustomerId { get; set; }

        public string? AddressType { get; set; }

        public string? Street { get; set; }

        public int? WardId { get; set; }

        public bool? IsDefault { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string? Note { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? AddressName { get; set; }

        public string? AddressPhoneNumber { get; set; }
    }

    public class CustomerExternalLoginDto
    {
        public int ExternalLoginId { get; set; }

        public int? CustomerId { get; set; }

        public string? PasswordHass { get; set; }

        public string? Provider { get; set; }

        public string? ProviderUserId { get; set; }

        public string? ProviderEmail { get; set; }

        public string? AvatarUrl { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
