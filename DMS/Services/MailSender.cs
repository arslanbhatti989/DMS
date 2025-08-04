using DMS.Repositories;
using System.Net.Mail;
using System.Net;

namespace DMS.Services
{
    public class MailSender : IMailSender
    {
        private readonly IConfiguration _configuration;
        public MailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> EmailwithAttachmentSendAsync(string email, string subject, string message, byte[] attachment, string attachmentName)
        {
            try
            {
                // Read email settings from configuration
                var emailSettings = new EmailSettings()
                {
                    SecretKey = _configuration.GetValue<string>("AppSettings:SecretKey"),
                    From = _configuration.GetValue<string>("AppSettings:EmailSettings:From"),
                    SmtpServer = _configuration.GetValue<string>("AppSettings:EmailSettings:SmtpServer"),
                    Port = _configuration.GetValue<int>("AppSettings:EmailSettings:Port"),
                    EnableSSL = _configuration.GetValue<bool>("AppSettings:EmailSettings:EnableSSL"),
                };

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(emailSettings.From);
                    mailMessage.Subject = subject;
                    mailMessage.Body = message;
                    mailMessage.IsBodyHtml = true; // Enable HTML formatting
                    mailMessage.To.Add(email);

                    // Attach file if available
                    if (attachment != null && attachment.Length > 0)
                    {
                        mailMessage.Attachments.Add(new Attachment(new MemoryStream(attachment), attachmentName));
                    }

                    using (SmtpClient smtpClient = new SmtpClient(emailSettings.SmtpServer, emailSettings.Port))
                    {
                        smtpClient.Credentials = new NetworkCredential(emailSettings.From, emailSettings.SecretKey);
                        smtpClient.EnableSsl = emailSettings.EnableSSL;
                        smtpClient.UseDefaultCredentials = false;

                        await smtpClient.SendMailAsync(mailMessage);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EmailSendAsync(string email, string subject, string message)
        {
            bool status = false;
            try
            {
                // Read email settings from configuration
                EmailSettings emailSettings = new EmailSettings()
                {
                    SecretKey = _configuration.GetValue<string>("AppSettings:SecretKey"),
                    From = _configuration.GetValue<string>("AppSettings:EmailSettings:From"),
                    SmtpServer = _configuration.GetValue<string>("AppSettings:EmailSettings:SmtpServer"),
                    Port = _configuration.GetValue<int>("AppSettings:EmailSettings:Port"),
                    EnableSSL = _configuration.GetValue<bool>("AppSettings:EmailSettings:EnableSSL"),
                };

                // Prepare email message
                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(emailSettings.From),
                    Subject = subject,
                    Body = message,
                };
                mailMessage.To.Add(email);

                // Send email via SMTP
                SmtpClient smtpClient = new SmtpClient(emailSettings.SmtpServer)
                {
                    Port = emailSettings.Port,
                    Credentials = new NetworkCredential(emailSettings.From, emailSettings.SecretKey),
                    EnableSsl = emailSettings.EnableSSL,
                };

                // Send email asynchronously
                await smtpClient.SendMailAsync(mailMessage);
                status = true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                status = false;
            }

            return status;
        }
    }

    // Define the EmailSettings class
    public class EmailSettings
    {
        public string SecretKey { get; set; }
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
    }
}
