using System.Net;
using System.Net.Mail;
namespace BucHelp.Data
{
    /// <summary>
    /// Email services for future use so users may have information sent to their emails.
    /// </summary>
    public class EmailServices
    {
        SmtpClient Client;

        public EmailServices()
        {
            Client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("Trapezoidsinc@gmail.com", "#TRAP!n") 
            };
        }

        public void SendEmail(string toEmail, string body, string subject)
        {
            MailAddress from = new MailAddress("Trapezoidsinc@gmail.com", "BucHelp - Trapeziods", System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress(toEmail);

            MailMessage message = new MailMessage(from, to);

            message.Body = body;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            Client.Send(message);

        }
    }
}
