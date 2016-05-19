using System.Web.Mvc;
using TrendPie.Models;

namespace TrendPie.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            var viewModel = new DashboardIndexViewModel();

            return View(viewModel);
        }
    }
}