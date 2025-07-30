namespace ShopeeFood_WebAPI.RequestModels.UserRequestDtos
{
    public class CustomerAddressRequestDto
    {
        public int AddressId { get; set; }

        //public int? CustomerId { get; set; }

        public string? AddressType { get; set; }

        public string? Street { get; set; }

        public int? WardId { get; set; }

        //public bool? IsDefault { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string? Note { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? AddressName { get; set; }

        public string? AddressPhoneNumber { get; set; }
    }
}
