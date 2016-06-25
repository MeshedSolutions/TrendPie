using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TrendPie.Models;
using TrendPie.Repositories;
using TrendPie.Services;

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
            {
                UserRepository.UpdateAmountPerCampaign(user.Id, user.AmountPerCampaign.Value);
                UserRepository.ClearAmountPerCampaignRequested(user.Id);
            }

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
            var campaigns = CampaignRepository.GetAllCreatedInLast30Days().OrderByDescending(i => i.DateCreated);

            foreach (var campaign in campaigns)
            {
                var payoutReport = new PayoutReportViewModel
                {
                    Campaign = campaign
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

        public FileContentResult Export(int campaignID)
        {
            var file = new StringBuilder();
            var campaign = CampaignRepository.GetById(campaignID);
            var userCampaigns = UserCampaignRepository.GetAllForCampaign(campaignID);

            if (userCampaigns.Any())
            {
                file.AppendLine(
                    "\"PayPal Email\"," +
                    "\"Rate\"," +
                    "\"Currency\""
                    );
            }

            foreach (var userCampaign in userCampaigns)
            {
                var user = UserRepository.GetByID(userCampaign.UserID);

                if (user != null)
                {
                    file.AppendLine(
                        "\"" + user.PayPalEmail + "\"," +
                        "\"" + user.AmountPerCampaign + "\"," +
                        "\"" + user.Country + "\""
                        );
                }
            }

            return File(new UTF8Encoding().GetBytes(file.ToString()), "text/csv", "mass pay " + campaign.Name + " " + campaign.ShortStartDate.Replace('/', '-') + ".csv");
        }

        public ActionResult EmailCampaignToList(int campaignID)
        {
            if (campaignID > 0)
            {
                EmailService.SendNewCampaignAvailableEmailToUsers(campaignID);
            }

            return RedirectToAction("CurrentCampaigns");
        }
    }
}