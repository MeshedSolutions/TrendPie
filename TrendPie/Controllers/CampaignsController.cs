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
            List<Campaign> viewModel = CampaignRepository.GetAllPending();

            return View(viewModel);
        }

        public ActionResult Live()
        {
            List<Campaign> viewModel = CampaignRepository.GetAllLive();

            return View(viewModel);
        }

        public ActionResult Complete()
        {
            List<Campaign> viewModel = CampaignRepository.GetAllComplete();

            return View(viewModel);
        }
    }
}
