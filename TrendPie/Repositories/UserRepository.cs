using System.Linq;
using TrendPie.Models;

namespace TrendPie.Repositories
{
    public class UserRepository
    {
        public static void Create(User user)
        {
            using (var db = new TrendPie_Entities())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public static User GetByEmail(string email)
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Users.FirstOrDefault(i => i.Email == email);
            }
        }
    }
}