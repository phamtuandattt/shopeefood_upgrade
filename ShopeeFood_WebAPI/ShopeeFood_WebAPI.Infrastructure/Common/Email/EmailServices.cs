using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ShopeeFood_WebAPI.Infrastructure.Common.Email
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailSettings _settings;

        public EmailServices(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            try
            {
                using var client = new SmtpClient(_settings.SmtpServer)
                {
                    Port = _settings.Port,
                    Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                    EnableSsl = _settings.EnableSsl
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                    Subject = subject,
                    Body = htmlContent,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
                Logger.Debug($"Email sent to {toEmail}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to send email to {toEmail}", ex);
                throw; // Rethrow to let the controller decide how to handle it
            }
        }
    }
}
