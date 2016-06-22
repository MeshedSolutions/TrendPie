using System.Collections.Generic;
using System.Web.Mvc;
using TrendPie.Models;
using TrendPie.Repositories;

namespace TrendPie.Controllers
{
    public class CampaignsController : Controller
    {
        public ActionResult Pending()
        {
            var viewModel = new List<Campaign>();

            if (Session != null)
            {
                var user = (User) Session["User"];

                viewModel = CampaignRepository.GetAllActivePendingForUser(user);
            }

            return View(viewModel);
        }

        public ActionResult Live()
        {
            var viewModel = new List<Campaign>();

            if (Session != null)
            {
                var user = (User) Session["User"];

                viewModel = CampaignRepository.GetAllActiveLiveForUser(user);
            }

            return View(viewModel);
        }

        public ActionResult Complete()
        {
            var viewModel = new List<Campaign>();

            if (Session != null)
            {
                var user = (User)Session["User"];
                viewModel = CampaignRepository.GetAllActiveCompleteForUser(user);
            }

            return View(viewModel);
        }

        public ActionResult Accept(int campaignID)
        {
            var user = (User) Session["User"];

            UserCampaignRepository.AcceptCampaign(campaignID, user.Id);

            List<Campaign> viewModel = CampaignRepository.GetAllPending();

            return RedirectToAction("Pending");
        }
    }
}
