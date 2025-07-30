using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.RequestDTOs.CustomerRequestDto
{
    public class CustomerLoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public CustomerLoginRequestDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
