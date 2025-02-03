using System;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.DAL.Models;

public partial class Shop
{
    public int ShopId { get; set; }

    public string ShopName { get; set; } = null!;

    public string? ShopImage { get; set; }

    public string? ShopAddress { get; set; }

    public string? ShopUptime { get; set; }

    public virtual ICollection<CityBusinessFieldsShop> CityBusinessFieldsShops { get; set; } = new List<CityBusinessFieldsShop>();
}
