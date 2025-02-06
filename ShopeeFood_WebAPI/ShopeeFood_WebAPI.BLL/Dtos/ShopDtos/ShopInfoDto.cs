using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.BLL.Dtos.ShopDtos
{
    public class ShopInfoDto
    {
        public int ShopId { get; set; }

        public string ShopName { get; set; } = null!;

        public string? ShopImage { get; set; }

        public string? ShopAddress { get; set; }

        public string? ShopUptime { get; set; }
    }
}
