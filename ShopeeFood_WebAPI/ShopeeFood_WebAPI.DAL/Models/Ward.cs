using System;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.DAL.Models;

public partial class Ward
{
    public int WardId { get; set; }

    public int? DistrictId { get; set; }

    public string? WardName { get; set; }

    public virtual District? District { get; set; }
}
