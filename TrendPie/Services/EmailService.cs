using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using TrendPie.Models;
using TrendPie.Repositories;

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
        public static void SendEmailToList(List<string> recipients, string from, string subject, string body)
        {
            if (recipients == null) return;

            foreach (var recipient in recipients)
            {
                var message = new MailMessage(from, recipient)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                SendMessage(message);
            }
        }
        public static void SendNewUserEmailToUser(User user)
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
        public static void SendPendingUserEmailToAdmin(User user)
        {
            var str = new StringBuilder();
            str.Append("A new user has signed up and is pending approval.<br/> ");
            str.Append(" <br/> ");
            str.Append("Name: " + user.FirstName + " " + user.LastName + "<br/> ");
            str.Append("Email: " + user.Email + "<br/> ");
            str.Append(" <br/> ");
            str.Append("To review the user, click the following link:<br/> ");
            str.Append("<a href='http://trendpie.com/Admin/UserProfile?userID=" + user.Id +"'>User Profile</a><br/> ");
            str.Append(" <br/>");
            str.Append("Thank you, <br/> ");
            str.Append("The TrendPie Team<br/> ");
            SendEmail(FromAddress, FromAddress, "There is a new pending user", str.ToString());
        }
        public static void SendAmountPerCampaignChangeRequestEmailToAdmin(User user)
        {
            var str = new StringBuilder();
            str.Append("A user has requested a change in their amount per campaign rate.<br/> ");
            str.Append(" <br/> ");
            str.Append("Name: " + user.FirstName + " " + user.LastName + "<br/> ");
            str.Append("Name: " + user.FirstName + " " + user.LastName + "<br/> ");
            str.Append("Current Amount: " + user.AmountPerCampaign + "<br/> ");
            str.Append("Requested Amount: " + user.AmountPerCampaignRequested + "<br/> ");
            str.Append(" <br/> ");
            str.Append("To review the user, click the following link:<br/> ");
            str.Append("<a href='http://trendpie.com/Admin/UserProfile?userID=" + user.Id + "'>User Profile</a><br/> ");
            str.Append(" <br/>");
            str.Append("Thank you, <br/> ");
            str.Append("The TrendPie Team<br/> ");
            SendEmail(FromAddress, FromAddress, "There is an amount per campaign change request", str.ToString());
        }
        public static void SendNewCampaignAvailableEmailToUsers(int campaignID)
        {
            var campaign = CampaignRepository.GetById(campaignID);
            var userEmails = UserRepository.GetAllActive().Select(i => i.Email).ToList();

            var str = new StringBuilder();
            str.Append("A user has requested a change in their amount per campaign rate.<br/> ");
            str.Append(" <br/> ");
            str.Append("Campaign: " + campaign.Name + "<br/> ");
            str.Append("Start Date: " + campaign.ShortStartDate + "<br/> ");
            str.Append(" <br/> ");
            str.Append("To view the campaign, login with the following link and view the pending campaigns list:<br/> ");
            str.Append("<a href='http://trendpie.azurewebsites.net/'>TrendPie</a><br/> ");
            str.Append(" <br/>");
            str.Append("Thank you, <br/> ");
            str.Append("The TrendPie Team<br/> ");
            SendEmailToList(userEmails, FromAddress, "There is a new TrendPie campaign available", str.ToString());
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
                    Port = emailPort,
                    EnableSsl = true
                };

                smtp.Send(message);
            }
        }
    }
}