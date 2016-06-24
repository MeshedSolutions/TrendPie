using System.Collections.Generic;

namespace TrendPie.Models
{
    public class PayoutReportViewModel
    {
        public Campaign Campaign { get; set; }
        public List<User> Users { get; set; }

        public PayoutReportViewModel()
        {
            Users = new List<User>();
        }
    }
}