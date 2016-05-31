﻿using System.Web.Mvc;
using TrendPie.Models;
using TrendPie.Repositories;

namespace TrendPie.Controllers
{
    public class HomeController : Controller
    {
        public string ErrorMessage = string.Empty;

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
            if (ModelState.IsValid)
            {
                var existingUser = UserRepository.GetByEmail(user.Email);

                if (existingUser != null)
                {
                    if (user.Password == existingUser.Password)
                    {
                        // Valid and approved user
                        if (user.Role == "user" && user.Status == "Approved")
                        {
                            Session["User"] = user;

                            return RedirectToAction("Index", "Dashboard");
                        }

                        // Admin user
                        if (user.Role == "admin")
                        {
                            Session["User"] = user;

                            return RedirectToAction("Index", "Admin");
                        }

                        // Valid but not approved user
                        ErrorMessage = "Your account has not been approved yet";
                    }
                    else
                    {
                        ErrorMessage = "Invalid username or password";
                    }
                }
                else
                {
                    ErrorMessage = "Invalid username or password";
                }
            }

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
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