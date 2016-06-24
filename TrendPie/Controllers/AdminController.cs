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
            if (user.AmountPerCampaign != null)
                UserRepository.UpdateAmountPerCampaign(user.Id, user.AmountPerCampaign.Value);

            return RedirectToAction("PendingInfluencers");
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

        public ActionResult EditCampaign(int campaignID)
        {
            Campaign campaign = CampaignRepository.GetById(campaignID);

            return View(campaign);
        }

        [HttpPost]
        public ActionResult EditCampaign(Campaign campaign)
        {
            CampaignRepository.Update(campaign);

            return RedirectToAction("CurrentCampaigns");
        }

        public ActionResult CurrentCampaigns()
        {
            var viewModel = CampaignRepository.GetAllActive();

            return View(viewModel);
        }

        public ActionResult CompleteCampaigns()
        {
            var viewModel = CampaignRepository.GetAllComplete();

            return View(viewModel);
        }

        public ActionResult PayoutReport()
        {
            var viewModel = new List<PayoutReportViewModel>();
            var campaigns = CampaignRepository.GetAll();

            foreach (var campaign in campaigns)
            {
                var payoutReport = new PayoutReportViewModel
                {
                    CampaignName = campaign.Name
                };

                var userCampaigns = UserCampaignRepository.GetAllForCampaign(campaign.Id);

                foreach (var userCampaign in userCampaigns)
                {
                    var user = UserRepository.GetByID(userCampaign.UserID);
                    if (user != null) payoutReport.Users.Add(user);
                }

                viewModel.Add(payoutReport);
            }

            return View(viewModel);
        }
    }
}