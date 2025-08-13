using ShopeeFood_WebAPI.BLL.Dtos.PageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.BLL.IServices
{
    public interface IPageServices
    {
        Task<List<PageSettingDto>> GetPages();
    }
}
