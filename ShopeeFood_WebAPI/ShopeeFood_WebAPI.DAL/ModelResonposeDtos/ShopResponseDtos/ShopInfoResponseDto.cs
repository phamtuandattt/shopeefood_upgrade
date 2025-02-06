using ShopeeFood_WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.DAL.ModelResonposeDtos.ShopResponseDtos
{
    public class ShopInfoResponseDto
    {
        public int CategoryId { get; set; }
        public int ShopId { get; set; }
        public string? CategoryName { get; set; }
        public List<CategoryItem> MenuDetailShops { get; set; } = new List<CategoryItem>();
    }

    public class CategoryItem
    {
        //public int CategoryId { get; set; }
        public int CategoryItemId { get; set; }
        public string? CategoryItemName { get; set; }
        public string? CategoryItemDescription { get; set; }
        public string? CategoryItemImage { get; set; }
        public int CategoryItemPrice { get; set; }
    }
}
