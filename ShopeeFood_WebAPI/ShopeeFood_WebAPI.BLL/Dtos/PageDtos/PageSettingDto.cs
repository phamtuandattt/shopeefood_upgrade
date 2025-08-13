using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.BLL.Dtos.PageDtos
{
    public class PageSettingDto
    {
        public int PageId { get; set; }

        public string? PagePath { get; set; }

        public string? PageName { get; set; }
    }
}
