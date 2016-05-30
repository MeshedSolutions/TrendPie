using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrendPie.Models;
using TrendPie.Repositories;

namespace TrendPie.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            string errorMessage = string.Empty;

            if (ModelState.IsValid)
            {
                var existingUser = UserRepository.GetByEmail(user.Email);

                if (existingUser != null)
                {
                    if (user.Password == existingUser.Password && existingUser.Approved)
                    {
                        Session["User"] = user;

                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        errorMessage = "Invalid username or password";
                    }
                }
                else
                {
                    errorMessage = "Invalid username or password";
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Agree)
                {
                    UserRepository.Create(user);
                }
            }

            return View(user);
        }
    }
}