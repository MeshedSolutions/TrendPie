using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrendPie.Models;

namespace TrendPie.Repositories
{
    public class UserCampaignRepository
    {
        public static void AcceptCampaign(int campaignID, int userID)
        {
            using (var db = new TrendPie_Entities())
            {
                var userCampaign = db.UserCampaigns.Create();
                var user = db.Users.Find(userID);

                userCampaign.CampaignID = campaignID;
                userCampaign.UserID = userID;
                userCampaign.AmountEarned = user.AmountPerCampaign;
                userCampaign.DateCreated = DateTime.Now;
                userCampaign.DateJoined = DateTime.Now;

                db.UserCampaigns.Add(userCampaign);
                db.SaveChanges();
            }
        }
    }
}