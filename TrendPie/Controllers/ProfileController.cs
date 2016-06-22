﻿using System.Collections.Generic;
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

        [HttpPost]
        public ActionResult UpdateWallet(User user)
        {
            var currentUser = (User) Session["User"];

            if (currentUser != null)
            {
                UpdateAmountPerCampaign(user, currentUser);
                UpdatePayPalEmail(user, currentUser);
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
        private static void UpdateAmountPerCampaign(User user, User currentUser)
        {
            if (user.AmountPerCampaign != currentUser.AmountPerCampaign)
            {
                UserRepository.UpdateStatus(currentUser.Id, "Pending");
                if (user.AmountPerCampaign != null)
                    UserRepository.UpdateAmountPerCampaign(currentUser.Id, user.AmountPerCampaign.Value);
            }
        }
    }
}