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
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Social()
        {
            List<SocialMediaAccount> viewModel = SocialMediaAccountRepository.GetAll();

            return View(viewModel);
        }
    }
}