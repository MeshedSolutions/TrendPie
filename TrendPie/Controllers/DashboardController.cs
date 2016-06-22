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
            if (Session != null)
            {
                var user = (User)Session["User"];

                viewModel.CampaignLiveList = CampaignRepository.GetTop5ActiveLiveForUser(user);
                viewModel.CampaignCompleteList = CampaignRepository.GetTop5ActiveCompleteForUser(user);
            }

            return View(viewModel);
        }
    }
}