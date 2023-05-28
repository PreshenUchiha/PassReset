using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PassReset.DB;
using PassReset.Helpers;
using PassReset.Models;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PassReset.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AccountController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    // Store user information in session
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("Username", user.Username);
                    // Successful login - Redirect to the Dashboard

                   // string tempPassword = GenerateRandomString();
                    string tempPassword = GenerateRandomString(4, "1234567890");
                    user.Password = tempPassword;

                    _dbContext.Users.Update(user);
                    _dbContext.SaveChanges();
                    TempData["DisplayTempPasswordMessage"] = "Temporary password generated: " + tempPassword;
                    return RedirectToAction("ChangePassword", "Home");

                }

                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            // Clear session data
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        //[HttpPut]
        //public  IActionResult ChangePassword(ChangePasswordModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Retrieve the user by username and verify the temporary password
        //        var user = _dbContext.Users.FirstOrDefault(i => i.Username == model.Username);
        //        if (user != null)
        //        {
        //            //Generate Temp password
        //            user.Password = GenerateRandomString(4, "1234567890");
        //            _dbContext.Users.Update(user);
        //          //  _dbContext.SaveChangesAsync();
        //            // Store the username in session
        //            //Session["Username"] = model.Username;

        //            //// Display the password in the success message
        //            //TempData["DisplayTempPasswordMessage"] = "Temporary password generated: " + model.Password;



        //        }



        //    }
        //    return View(model);

        //}

        [HttpPut]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _dbContext.Users.FirstOrDefault(i => i.Username == model.Username);
                if (user != null)
                {
                    // Generate Temp password
                    string tempPassword = GenerateRandomString(4,"1234567890");
                    user.Password = tempPassword;

                    _dbContext.Users.Update(user);
                    _dbContext.SaveChanges();

                    // Store the username and temporary password in session
                    HttpContext.Session.SetString("Username", model.Username);
                    TempData["DisplayTempPasswordMessage"] = "Temporary password generated: " + tempPassword;

                    return RedirectToAction("Login");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }

            return View(model);
        }


        //static string GenerateRandomString()
        //{
        //    Random ran = new Random();

        //    String b = "abcdefghijklmnopqrstuvwxyz";

        //    int length = 6;

        //    String random = "";

        //    for (int i = 0; i < length; i++)
        //    {
        //        int a = ran.Next(26);
        //        random = random + b.ElementAt(a);
        //    }
        //    return random;
        // }

        static string GenerateRandomString(int length, string characters)
        {
            Random random = new Random();
            string randomString = new string(Enumerable.Repeat(characters, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return randomString;
        }

        // GET: /Account/Signup
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        // POST: /Account/Signup
        [HttpPost]
        public IActionResult Signup(SignupModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password
                };

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                // Successful signup
                return RedirectToAction("Login");
            }

            return View(model);
        }
    }



}
