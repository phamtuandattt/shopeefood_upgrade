using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.RequestDTOs.CustomerRequestDto
{
    public class ResetpasswordRequestDto
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
