using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrendPie.Models;
using TrendPie.Repositories;

namespace TrendPie.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ActiveInfluencers()
        {
            List<User> viewModel = UserRepository.GetAllActive();

            return View(viewModel);
        }

        public ActionResult PendingInfluencers()
        {
            List<User> viewModel = UserRepository.GetAllPending();

            return View(viewModel);
        }

        public ActionResult UserProfile(int userID)
        {
            User viewModel = UserRepository.GetByID(userID);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UserProfile(User user)
        {
            UserRepository.UpdateStatus(user.Id, user.Status);

            return View(user);
        }

        public ActionResult CreateCampaign()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCampaign(Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                CampaignRepository.Create(campaign);
            }
            return View(campaign);
        }
    }
}