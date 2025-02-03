using System;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.DAL.Models;

public partial class CityBusinessFieldsShop
{
    public int CityId { get; set; }

    public int FieldId { get; set; }

    public int ShopId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual BusinessField Field { get; set; } = null!;

    public virtual Shop Shop { get; set; } = null!;
}
