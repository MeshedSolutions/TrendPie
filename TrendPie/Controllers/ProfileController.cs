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
            var user = new User();

            if (Session != null) user = (User) Session["User"];

            return View(user);
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

        [HttpPost]
        public ActionResult UpdateWallet(User user)
        {
            var currentUser = (User) Session["User"];

            if (currentUser != null)
            {
                RequestAmountPerCampaignUpdate(user, currentUser);
                UpdatePayPalEmail(user, currentUser);
                currentUser = UserRepository.GetByID(currentUser.Id);
                Session["User"] = currentUser;
            }


            return RedirectToAction("Wallet");
        }

        private static void UpdatePayPalEmail(User user, User currentUser)
        {
            if (user.PayPalEmail != currentUser.PayPalEmail)
            {
                UserRepository.UpdatePayPalEmail(currentUser.Id, user.PayPalEmail);
            }
        }
        private static void RequestAmountPerCampaignUpdate(User user, User currentUser)
        {
            if (user.AmountPerCampaign != currentUser.AmountPerCampaign)
            {
                if (user.AmountPerCampaign != null)
                    UserRepository.RequestAmountPerCampaignUpdate(currentUser.Id, user.AmountPerCampaign.Value);
            }
        }
    }
}