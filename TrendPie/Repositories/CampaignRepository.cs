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
        public static List<Campaign> GetTop5ActiveLive()
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Campaigns.Where(i => i.Active == "true" && i.Status == "live").Take(5).ToList();
            }
        }
        public static List<Campaign> GetTop5ActiveComplete()
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Campaigns.Where(i => i.Active == "true" && i.Status == "complete").Take(5).ToList();
            }
        }
        public static List<Campaign> GetAllPending()
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Campaigns.Where(i => i.Status == "Pending").ToList();
            }
        }
        public static List<Campaign> GetAllComplete()
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Campaigns.Where(i => i.Status == "Complete").ToList();
            }
        }
        public static List<Campaign> GetAllLive()
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Campaigns.Where(i => i.Status == "Live").ToList();
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