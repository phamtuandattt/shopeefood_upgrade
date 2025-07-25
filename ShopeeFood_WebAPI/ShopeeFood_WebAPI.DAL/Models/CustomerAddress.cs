using System;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.DAL.Models;

public partial class CustomerAddress
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

    public virtual Customer? Customer { get; set; }
}
