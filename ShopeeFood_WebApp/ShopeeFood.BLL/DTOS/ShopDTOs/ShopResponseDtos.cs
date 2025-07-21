using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.DTOS.ShopDTOs
{
    public class ShopResponseDtos
    {
        public int CityID { get; set; }
        public int FieldID { get; set; }
        public int ShopID { get; set; }
        public string ShopName { get; set; } = string.Empty;
        public int TotalRecords { get; set; }
    }
}
