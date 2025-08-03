using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.Infrastructure.Common.Email
{
    public interface IEmailServices
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlContent);
    }
}
