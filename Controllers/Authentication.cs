using Microsoft.AspNetCore.Mvc;
using HarborView_Inn.Models;
using System.Web;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;
using RestSharp;

namespace HarborView_Inn.Controllers
{
    public class Authentication : Controller
    {
        public IActionResult Login()
        {
           
            return View();
           
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Cook");
            Response.Cookies.Delete("Cook1");
            Response.Cookies.Delete("Cook2");

            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete("AspNetCore.Session");
            ViewBag.isAuthentication = null;

            // Redirect or return a view
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ViewBag.allReq = "All fields are required!";
                return View();
            }

            if (Email == "admin@hotel.com" && Password == "admin123")
            {
                HttpContext.Session.SetString("admin", "ok");

                return RedirectToAction("admin", "Admin");
            }

            // Authenticate
            UserRepository temp = new UserRepository();
            User t = new User();
            t.Email = Email;
            t.Password = Password;
            int[] isAuthenticated = temp.authenticateUser(t);

            if (isAuthenticated[0] == 0)
            {
                ViewBag.wrongCred = "Invalid email or password.";
                return View();
            }
            else
            {
                WebProjectAuthenticateUserContext context = new WebProjectAuthenticateUserContext();
                User? user = context.Users.Find(Email);
                var cookieOptions1 = new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(1), // Set the expiration date of the cookie
                    HttpOnly = true // Specify if the cookie is accessible only through HTTP
                };

                HttpContext.Response.Cookies.Append("Cook2", user.Location, cookieOptions1);

                int key = isAuthenticated[1];
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(1), // Set the expiration date of the cookie
                    HttpOnly = true // Specify if the cookie is accessible only through HTTP
                };

                HttpContext.Session.SetString("UserEmail", Email);
                HttpContext.Session.SetString("UserLocation", user.Location);

                HttpContext.Response.Cookies.Append("Cook", Email, cookieOptions);
                ViewBag.isAuthenticated = HttpContext.Session.GetString("UserEmail");

                ViewBag.em = HttpContext.Request.Cookies["Cook"];

                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string Name, string Email,string Password, string Confirm_Password,string PhoneNo)
        {
            // if any parameter is null return error 
            if (Email == null || Password == null || Name == null || PhoneNo == null)
            {
                ViewBag.allReq = "All Fields are required!";
                return View();
            }
            if (!Password.Equals(Confirm_Password))
            {
                ViewBag.PassNotMatchError = "Passwords do not match.";
                return View();
            }    

            // check if email already exists return error 
            UserRepository temp=new UserRepository();
            User t = new User();
            t.Name = Name;
            t.Email = Email;
            t.Password= Password;
            t.PhoneNo = PhoneNo;
            string loc = Request.Form["location"];
            t.Location = loc;
            int[] isAdd = new int[2];
            isAdd = temp.addUser(t);
            if (isAdd[0]==0) 
            {
                TempData["signinFromSignup"] = "Email Exists. Sign In instead";
                return RedirectToAction("login");
            }
            else
            {
                int key = isAdd[1];
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(1), // Set the expiration date of the cookie
                    HttpOnly = true // Specify if the cookie is accessible only through HTTP
                };
                string loc1 = Request.Form["location"];
                HttpContext.Response.Cookies.Append("Cook1",loc1, cookieOptions);

                HttpContext.Response.Cookies.Append("Cook", Email, cookieOptions);

                ViewBag.em = Request.Cookies["Cook"];
                //if email is new

                //var cookieOptions = new CookieOptions
                //{
                //    Expires = DateTime.Now.AddHours(1), // Set the expiration date of the cookie
                //    HttpOnly = true // Specify if the cookie is accessible only through HTTP
                //};


                //HttpContext.Response.Cookies.Append("Cook", System.DateTime.Now.ToString(),cookieOptions);
                //TempData["Alert"] = "Welcome to new User, Sign Up Successfully !! ";
                object data = "Account Created";
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
