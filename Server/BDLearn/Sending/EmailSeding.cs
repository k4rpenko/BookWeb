using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace BDLearn.Sending
{
    public class EmailSeding
    {
        SmtpClient smtpClient;
        string SenderEmail;

        public EmailSeding()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            SenderEmail = builder.GetSection("Mailhog:SenderEmail").Value;


            smtpClient = new SmtpClient(builder.GetSection("Mailhog:Host").Value)
            {
                Port = int.Parse(builder.GetSection("Mailhog:Port").Value),
                EnableSsl = true,
            };
        }

        public void PasswordCheckEmail(string EmailTo)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(SenderEmail),
                    Subject = "Test Email",
                    Body = "This is a test email.",
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(EmailTo);
                smtpClient.Send(mailMessage);
            }
            catch (SmtpException smtpEx)
            {
                throw new Exception($"SMTP помилка: {smtpEx.Message}", smtpEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Не вдалося підключитися до SMTP", ex);
            }
        }
    }
}
