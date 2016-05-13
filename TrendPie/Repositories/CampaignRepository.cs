using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TrendPie.Models;

namespace TrendPie.Repositories
{
    public class CampaignRepository
    {
        public static List<Campaign> GetAll()
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Campaigns.ToList();
            }
        }
        public static Campaign GetById(int campaignId)
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Campaigns.Find(campaignId);
            }
        }
        public static void Create(Campaign campaign)
        {
            using (var db = new TrendPie_Entities())
            {
                db.Campaigns.Add(campaign);
                db.SaveChanges();
            }
        }
        public static Campaign Update(Campaign campaign)
        {
            using (var db = new TrendPie_Entities())
            {
                db.Entry(campaign).State = EntityState.Modified;
                db.SaveChanges();
            }

            return campaign;
        }
    }
}