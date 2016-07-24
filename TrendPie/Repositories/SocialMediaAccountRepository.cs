using System.Collections.Generic;
using System.Linq;
using TrendPie.Models;

namespace TrendPie.Repositories
{
    public class SocialMediaAccountRepository
    {
        public static List<SocialMediaAccount> GetAll()
        {
            using (var db = new TrendPie_dbEntities())
            {
                return db.SocialMediaAccounts.ToList();
            }
        }
    }
}