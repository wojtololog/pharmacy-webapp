using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace PharmacyWebApp.Services
{
    public class MailService
    {
        public async Task SendGeneratedKey(string receiver, string key)
        {
            var client = new SmtpClient();

            var message = new MailMessage();

            message.From = new MailAddress(ConfigurationManager.AppSettings.Get("Email"));
            message.To.Add(new MailAddress(receiver));

            message.Subject = "Klucz apteki";


            message.Body = "Twój klucz:" + System.Environment.NewLine + key + System.Environment.NewLine + "W przypadku utraty klucza prosimy o kontakt z administratorem.";
           await client.SendMailAsync(message);
        }

        public async Task SendConfirmation(string receiver, string callbackUrl)
        {
            var client = new SmtpClient();

            var message = new MailMessage();

            message.From = new MailAddress(ConfigurationManager.AppSettings.Get("Email"));
            message.To.Add(new MailAddress(receiver));

            message.Subject = "Potwierdzenie rejestracji";
            message.IsBodyHtml = true;
            message.Body = "Prosimy o potwierdzenie rejestracji klikając <a href=" + callbackUrl + ">tutaj</a>"; 

            await client.SendMailAsync(message);
            //await client.SendMailAsync(
            //    ConfigurationManager.AppSettings.Get("Email"),
            //    receiver,
            //   "Potwierdzenie rejestracji",
            //   "Prosimy o potwierdzenie rejestracji klikając w odnośnik <a href="+callbackUrl+"></a>");
        }
    }
}