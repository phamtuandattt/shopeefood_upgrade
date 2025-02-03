using System;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.DAL.Models;

public partial class BusinessField
{
    public int FieldId { get; set; }

    public string FieldName { get; set; } = null!;

    public virtual ICollection<CityBusinessFieldsShop> CityBusinessFieldsShops { get; set; } = new List<CityBusinessFieldsShop>();

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
