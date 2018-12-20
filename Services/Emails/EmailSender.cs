using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DeliveryAppBackend.Services.Emails
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailConfirmationAsync(string email, string callBackUrl)
        {
            await Task.Run(() => Execute(email,"Successfull Registration", "Thanks for registering with us  please click the link below to confirm registration "+ callBackUrl));
        }

        public async Task SendEmailConfirmationAsync(string email, string callBackUrl, string password)
        {
            await Task.Run(() => Execute(email, "Successfull Registration",
                $"Thanks for registering with us your username is {email} and your password is {password}," +
                $" please click the link below to confirm registration { callBackUrl}"));
        }

        public async Task SendMessageAsyc(string email, string subject, string message)
        {
            await Task.Run(() => Execute(email, subject, message));
        }
        private async Task Execute(string email, string subject, string message)
        {
            MailMessage mail = new MailMessage();
            string fromEmail = "test@gmail.com";
            string fromPW = null;
            string toEmail = email;
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = message;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            using (SmtpClient smtpClient = new SmtpClient("localhost", 1025))
            {
                smtpClient.EnableSsl = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, fromPW);

              await  smtpClient.SendMailAsync(mail.From.ToString(), mail.To.ToString(),
                                mail.Subject, mail.Body);
            }
        }
    }
}
