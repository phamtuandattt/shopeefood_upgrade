using System;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.DAL.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Avata { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public string? PasswordHash { get; set; }

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();

    public virtual ICollection<CustomerExternalLogin> CustomerExternalLogins { get; set; } = new List<CustomerExternalLogin>();
}
