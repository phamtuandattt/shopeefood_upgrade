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
        //private readonly EmailSettings _settings;

        //public EmailServices(IOptions<EmailSettings> settings)
        //{
        //    _settings = settings.Value;
        //}


        public async Task SendEmailAsync(string userEmail, string userName, string subject, string resetLink, EmailSettings emailSettings)
        {
            try
            {
                using var client = new SmtpClient(emailSettings.SmtpServer)
                {
                    Port = emailSettings.Port,
                    Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password),
                    EnableSsl = emailSettings.EnableSsl
                };

                string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplate", "ResetPasswordEmailTemplate.html");

                var placeholders = new Dictionary<string, string>
                {
                    { "UserName", userName},
                    { "ResetLink", resetLink }
                };

                string body = File.ReadAllText(templatePath);

                foreach (var placeholder in placeholders)
                {
                    body = body.Replace("{{" + placeholder.Key + "}}", placeholder.Value);
                }

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(userEmail);

                await client.SendMailAsync(mailMessage);
                Logger.Debug($"Email sent to {userEmail}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to send email to {userEmail}", ex);
                throw; // Rethrow to let the controller decide how to handle it
            }
        }
    }
}
