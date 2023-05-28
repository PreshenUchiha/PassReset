using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PassReset.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PassReset.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Dashboard()
        {
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                // User is not authenticated - Redirect to login
                return RedirectToAction("Login", "Account");
            }

            // Retrieve user information from session
            var userId = HttpContext.Session.GetString("UserId");
            var email = HttpContext.Session.GetString("Username");

            // Pass the user information to the view or perform any other required actions

            return View();
        }

    }
}
