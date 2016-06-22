using System.Collections.Generic;
using System.Web.Mvc;
using TrendPie.Models;
using TrendPie.Repositories;

namespace TrendPie.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Wallet()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            var user = new User();

            if (Session != null) user = (User) Session["User"];

            return View(user);
        }
        [HttpPost]
        public ActionResult UserProfile(User user)
        {
            UserRepository.UpdateUserProfile(user);

            return View(user);
        }

        public ActionResult Social()
        {
            List<SocialMediaAccount> viewModel = SocialMediaAccountRepository.GetAll();

            return View(viewModel);
        }
    }
}