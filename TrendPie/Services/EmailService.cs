using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using TrendPie.Models;

namespace TrendPie.Services
{
    public class EmailService
    {
        private static readonly string FromAddress;

        static EmailService()
        {
            FromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
        }

        public static void SendEmail(string to, string from, string subject, string body)
        {
            var emailTo = new MailAddress(to);
            var emailFrom = new MailAddress(from);
            var message = new MailMessage(emailFrom, emailTo)
            {
                Subject = subject, 
                Body = body, 
                IsBodyHtml = true
            };

            SendMessage(message);
        }
        public static void SendEmail(List<string> recipients, string from, string subject, string body)
        {
            if (recipients == null) return;

            var message = new MailMessage
            {
                From = new MailAddress(@from),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            foreach (var recipient in recipients)
            {
                message.To.Add(recipient);
            }


            SendMessage(message);
        }
        public void SendNewUserEmail(User user)
        {
            var str = new StringBuilder();
            str.Append("Thank you for your interest in TrendPie.<br/> ");
            str.Append(" <br/> ");
            str.Append("Your account is currently under review.<br/> ");
            str.Append(" <br/> ");
            str.Append("We will send you an email once you have been approved.<br/> ");
            str.Append(" <br/>");
            str.Append("Thank you, <br/> ");
            str.Append("The TrendPie Team<br/> ");
            SendEmail(user.Email, FromAddress, "TrendPie signup complete", str.ToString());
        }

        private static void SendMessage(MailMessage message)
        {
            string serverAddress = ConfigurationManager.AppSettings["EmailServerAddress"];
            string userName = ConfigurationManager.AppSettings["EmailUserName"];
            string password = ConfigurationManager.AppSettings["EmailPassword"];
            int emailPort = int.Parse(ConfigurationManager.AppSettings["EmailServerPort"]);

            if (!string.IsNullOrEmpty(serverAddress))
            {
                var smtp = new SmtpClient(serverAddress)
                {
                    Credentials = new System.Net.NetworkCredential(userName, password),
                    Port = emailPort
                };

                smtp.Send(message);
            }
        }
    }
}