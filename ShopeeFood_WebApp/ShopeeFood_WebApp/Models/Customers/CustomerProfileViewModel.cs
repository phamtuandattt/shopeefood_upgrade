using Newtonsoft.Json;

namespace ShopeeFood_WebApp.Models.Customers
{
    public class CustomerProfileViewModel
    {
        public CustomerInfoViewModel CustomerInfo { get; set; }

        public List<CustomerAddressViewModel> CustomerAddresses { get; set; }

        public List<CustomerExternalLoginViewModel> CustomerExternalLogins { get; set; }
    }

    public class CustomerAddressViewModel
    {
        [JsonProperty("addressId")]
        public int AddressId { get; set; }

        [JsonProperty("customerId")]
        public int? CustomerId { get; set; }

        [JsonProperty("addressType")]
        public string? AddressType { get; set; }

        [JsonProperty("street")]
        public string? Street { get; set; }

        [JsonProperty("wardId")]
        public int? WardId { get; set; }

        [JsonProperty("isDefault")]
        public bool? IsDefault { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("note")]
        public string? Note { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("addressName")]
        public string? AddressName { get; set; }

        [JsonProperty("addressPhoneNumber")]
        public string? AddressPhoneNumber { get; set; }
    }

    public class CustomerExternalLoginViewModel
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
