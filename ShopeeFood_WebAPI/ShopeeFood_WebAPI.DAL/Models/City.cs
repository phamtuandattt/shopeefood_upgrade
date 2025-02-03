using System;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.DAL.Models;

public partial class City
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public virtual ICollection<CityBusinessFieldsShop> CityBusinessFieldsShops { get; set; } = new List<CityBusinessFieldsShop>();

    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    public virtual ICollection<BusinessField> Fields { get; set; } = new List<BusinessField>();
}
