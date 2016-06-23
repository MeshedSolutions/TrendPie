using System.Collections.Generic;

namespace TrendPie.Models
{
    public class PayoutReportViewModel
    {
        public string CampaignName { get; set; }
        public List<User> Users { get; set; }

        public PayoutReportViewModel()
        {
            Users = new List<User>();
        }
    }
}