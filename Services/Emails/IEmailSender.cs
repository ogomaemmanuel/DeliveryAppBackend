using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryAppBackend.Services.Emails
{
   public interface IEmailSender
    {
        Task SendMessageAsyc(string email, string subject, string message);
        Task SendEmailConfirmationAsync(String email, String callBackUrl );
        Task SendEmailConfirmationAsync(String email, String callBackUrl,string password );
    }
}
