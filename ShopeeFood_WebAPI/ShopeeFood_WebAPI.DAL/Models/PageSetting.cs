using System;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.DAL.Models;

public partial class PageSetting
{
    public int PageId { get; set; }

    public string? PagePath { get; set; }

    public string? PageName { get; set; }
}
