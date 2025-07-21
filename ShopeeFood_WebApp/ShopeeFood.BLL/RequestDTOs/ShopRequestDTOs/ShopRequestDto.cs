using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.RequestDTOs.ShopRequestDTOs
{
    public class ShopRequestDto
    {
        public int CityID { get; set; } = 1;
        public int FieldID { get; set; } = 1;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}
