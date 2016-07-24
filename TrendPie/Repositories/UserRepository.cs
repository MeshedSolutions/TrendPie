using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TrendPie.Models;

namespace TrendPie.Repositories
{
    public class UserRepository
    {
        public static void Create(User user)
        {
            user.Status = "Pending";

            using (var db = new TrendPie_dbEntities())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public static User GetByEmail(string email)
        {
            using (var db = new TrendPie_dbEntities())
            {
                return db.Users.FirstOrDefault(i => i.Email == email);
            }
        }
        public static User GetByID(int userID)
        {
            using (var db = new TrendPie_dbEntities())
            {
                return db.Users.Find(userID);
            }
        }
        public static List<User> GetAllActive()
        {
            using (var db = new TrendPie_dbEntities())
            {
                return db.Users.Where(i => i.Status == "Approved").ToList();
            }
        }
        public static List<User> GetAllPending()
        {
            using (var db = new TrendPie_dbEntities())
            {
                return db.Users.Where(i => i.Status == "Pending").ToList();
            }
        }
        public static void UpdateStatus(int userID, string status)
        {
            using (var db = new TrendPie_dbEntities())
            {
                var user = db.Users.Find(userID);

                if (user != null)
                {
                    user.Status = status;

                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
        public static void UpdateUserProfile(User user)
        {
            using (var db = new TrendPie_dbEntities())
            {
                var userProfile = db.Users.Find(user.Id);

                userProfile.FirstName = user.FirstName;
                userProfile.LastName = user.LastName;
                userProfile.DOB = user.DOB;
                userProfile.Email = user.Email;
                userProfile.Address1 = user.Address1;
                userProfile.Address2 = user.Address2;
                userProfile.Country = user.Country;
                userProfile.State = user.State;
                userProfile.Zip = user.Zip;
                userProfile.PreferredContact = user.PreferredContact;

                db.SaveChanges();
            }
        }
        public static void UpdatePayPalEmail(int userID, string payPalEmail)
        {
            using (var db = new TrendPie_dbEntities())
            {
                var user = db.Users.Find(userID);

                if (user != null)
                {
                    user.PayPalEmail = payPalEmail;
                    db.SaveChanges();
                }
            }
        }
        public static void UpdateAmountPerCampaign(int userID, int amountPerCampaign)
        {
            using (var db = new TrendPie_dbEntities())
            {
                var user = db.Users.Find(userID);

                if (user != null)
                {
                    user.AmountPerCampaign = amountPerCampaign;
                    db.SaveChanges();
                }
            }
        }
        public static void RequestAmountPerCampaignUpdate(int userID, int amountPerCampaign)
        {
            using (var db = new TrendPie_dbEntities())
            {
                var user = db.Users.Find(userID);

                if (user != null)
                {
                    user.AmountPerCampaignRequested = amountPerCampaign;
                    db.SaveChanges();
                }
            }
        }
        public static void ClearAmountPerCampaignRequested(int userID)
        {
            using (var db = new TrendPie_dbEntities())
            {
                var user = db.Users.Find(userID);

                if (user != null)
                {
                    user.AmountPerCampaignRequested = null;
                    db.SaveChanges();
                }
            }
        }
    }
}