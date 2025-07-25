using System;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.DAL.Models;

public partial class CustomerExternalLogin
{
    public int ExternalLoginId { get; set; }

    public int? CustomerId { get; set; }

    public string? Provider { get; set; }

    public string? ProviderUserId { get; set; }

    public string? ProviderEmail { get; set; }

    public string? AvatarUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Customer? Customer { get; set; }
}
