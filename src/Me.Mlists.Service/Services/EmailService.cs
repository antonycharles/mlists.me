using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Me.Mlists.Service.Services
{
    public class EmailService : IEmailService
    {
        private string host;
        private int port;
        private bool enableSSL;
        private string userName;
        private string password;

        public EmailService(string host, int port, bool enableSSL, string userName, string password)
        {
            this.host = host;
            this.port = port;
            this.enableSSL = enableSSL;
            this.userName = userName;
            this.password = password;
        }

        // Use our configuration to send the email by using SmtpClient
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential(userName, password);

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Host = host;
                smtpClient.Port = port;
                smtpClient.EnableSsl = enableSSL;
                smtpClient.Timeout = 20000;

                await smtpClient.SendMailAsync(
                    new MailMessage(userName, email, subject, htmlMessage) { IsBodyHtml = true }
                );
            };

        }
    }
}
