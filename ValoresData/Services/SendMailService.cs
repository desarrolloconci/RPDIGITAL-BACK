using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Models;

namespace ValoresData.Services
{
    public class SendMailService : ISendMailService
    {   public GmailSettingModel _gmailsettings {get;}
        public SendMailService(IOptions<GmailSettingModel> gmailsettings)
        {
            _gmailsettings= gmailsettings.Value;
        }
        public void sendEmail(string subject, string to, string body)
        {
           
            try
            {

                var fromEmail = _gmailsettings.userName;
                var password = _gmailsettings.Password;

                var message = new MailMessage();
                message.From = new MailAddress(fromEmail);
                message.Subject = subject;
                message.To.Add(new MailAddress(to));
                message.Body = body;
                message.IsBodyHtml = true;

                var smptClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = _gmailsettings.Port,
                    Credentials = new NetworkCredential(fromEmail, password),
                    EnableSsl = true
                };
                smptClient.Send(message);

            }
            catch (Exception ex) {

                throw new Exception("No se pudo enviar mail", ex);
            }
        }
    }
}
