using System;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.DAL.Models;

public partial class MenuShop
{
    public int CategoryId { get; set; }

    public int? ShopId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<MenuDetailShop> MenuDetailShops { get; set; } = new List<MenuDetailShop>();
}
