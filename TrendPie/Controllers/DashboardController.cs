using System.Web.Mvc;
using TrendPie.Models;
using TrendPie.Repositories;

namespace TrendPie.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            var viewModel = new DashboardIndexViewModel();

            viewModel.CampaignLiveList = CampaignRepository.GetTop5ActiveLive();
            viewModel.CampaignCompleteList = CampaignRepository.GetTop5ActiveComplete();

            return View(viewModel);
        }
    }
}