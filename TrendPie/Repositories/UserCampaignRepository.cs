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

                var campaign = db.Campaigns.Find(campaignID);

                if (campaign != null)
                {
                    int currentAmountTowardsBudget = 0;
                    var userCampaigns = db.UserCampaigns.Where(i => i.CampaignID == campaignID);

                    foreach (var userCampaign1 in userCampaigns)
                    {
                        var campaignUser = db.Users.Find(userCampaign1.UserID);
                        if (campaignUser != null)
                        {
                            currentAmountTowardsBudget += campaignUser.AmountPerCampaign.HasValue ? campaignUser.AmountPerCampaign.Value : 0;
                        }
                    }

                    if (currentAmountTowardsBudget >= campaign.Budget)
                    {
                        campaign.Status = "complete";
                        db.SaveChanges();
                    }
                }
            }
        }
        public static List<UserCampaign> GetAllForCampaign(int campaignID)
        {
            using (var db = new TrendPie_Entities())
            {
                return db.UserCampaigns.Where(i => i.CampaignID == campaignID).ToList();
            }
        }
    }
}