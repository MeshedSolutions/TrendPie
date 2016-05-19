using System.Collections.Generic;

namespace TrendPie.Models
{
    public class DashboardIndexViewModel
    {
        public int UserID { get; set; }
        public string EarnedAllTime { get; set; }
        public string EarnedPast30Days { get; set; }
        public string EarnedPast7Days { get; set; }
        public List<Campaign> CampaignLiveList { get; set; }
        public List<Campaign> CampaignCompleteList { get; set; }

        public DashboardIndexViewModel()
        {
            CampaignLiveList = new List<Campaign>();
            CampaignCompleteList = new List<Campaign>();
        }
    }
}