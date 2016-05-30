using System.Collections.Generic;
using System.Linq;
using TrendPie.Models;

namespace TrendPie.Repositories
{
    public class UserRepository
    {
        public static void Create(User user)
        {
            user.Status = "Pending";

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
        public static List<User> GetAllActive()
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Users.Where(i => i.Status == "Active").ToList();
            }
        }
        public static List<User> GetAllPending()
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Users.Where(i => i.Status == "Pending").ToList();
            }
        }
    }
}