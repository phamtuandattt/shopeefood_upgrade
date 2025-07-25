using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.BLL.Dtos.CustomerDtos
{
    public class CustomerDto
    {
        //public Guid Id { get; set; } = Guid.NewGuid();
        //public string FullName { get; set; }
        //public string Email { get; set; }
        //public string? Provider { get; set; } // e.g., Google, Facebook (optional)
        //public string? ProviderId { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CustomerId { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
        public string? PasswordHash { get; set; }

        public string? Avata { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
}
