using System;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.DAL.Models;

public partial class MenuDetailShop
{
    public int CategoryItemId { get; set; }

    public int? CategoryId { get; set; }

    public string? CategoryItemName { get; set; }

    public string? CategoryItemDescription { get; set; }

    public string? CategoryItemImage { get; set; }

    public int? CategoryItemPrice { get; set; }

    public virtual MenuShop? Category { get; set; }
}
