using CleanArchitecture.WebApi1.Application.DTOs.Email;
using CleanArchitecture.WebApi1.Application.Exceptions;
using CleanArchitecture.WebApi1.Application.Interfaces;
using CleanArchitecture.WebApi1.Domain.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace CleanArchitecture.WebApi1.Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        public MailSettings _mailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendAsync(EmailRequest request)
        {
          
            try
            {
                using (var client = new System.Net.Mail.SmtpClient())
                {

                    var fromAddress = new MailAddress(_mailSettings.SmtpUser);
                     string fromPassword = _mailSettings.SmtpPass;

                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(fromAddress.Address, fromPassword);
                    client.Host = _mailSettings.SmtpHost;
                    client.Port = _mailSettings.SmtpPort;
                    client.EnableSsl = true;



                    using var emailMessage = new MailMessage()
                    {
                        To = { new MailAddress(request.To) },
                        From = fromAddress, // with @gmail.com
                        Subject = request.Subject,
                        Body = request.Body,
                        IsBodyHtml = true
                    };

                    await client.SendMailAsync(emailMessage);

                  
                }
             
            }
            catch (System.Exception ex)
            {

                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
