using Microsoft.AspNetCore.Mvc;
using HarborView_Inn.Models;
using System.Web;
using System.Net;

namespace HarborView_Inn.Controllers
{
    public class Authentication : Controller
    {
        public IActionResult Login()
        {
            return View();
           
        }

        [HttpPost]
        public IActionResult Login(string Email,string Password)
        {
            // if any parameter is null return error 
            if (Email == null || Password == null)
            { 
                ViewBag.q = "You Left One of the Field Empty !!";
                return View();
            }

            // Authenticate
            UserRepository temp = new UserRepository();
            User t = new User();
            t.Email = Email;
            t.Password = Password;
            int[] isAuthenticated = new int[2];
            isAuthenticated=temp.authenticateUser(t);
            if (isAuthenticated[0]==0)
            {
                ViewBag.p = "Invalid Credentials !!";
                return View();
            }
            else
            {
                int key = isAuthenticated[1];
                var em = Email.Split('@');
                string firstDateTime = Request.Cookies[$"{key.ToString()}"];
                //TempData["Alert1"] = $"Welcome again , You Visited Us At {firstDateTime} For the First Time.";
                object data = $"Welcome {em[0].ToUpper()} , You Visited us at {firstDateTime} First Time !!";
                return View(data);
            }
           

        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string Name, string Email,string Password, string Confirm_Password)
        {
            // if any parameter is null return error 
            if (Email == null || Password == null || Name == null)
            {
                ViewBag.r = "All Fields are required!";
                return View();
            }
            if (!Password.Equals(Confirm_Password))
            {
                ViewBag.PassNotMatchError = "Passwords do not match.";
                return View();
            }    

            // check if email already exists return error 
            UserRepository temp=new UserRepository();
            User t=new User();
            t.Name = Name;
            t.Email = Email;
            t.Password= Password;
            int[] isAdd = new int[2];
            isAdd=temp.addUser(t);
            if (isAdd[0]==0) 
            {
                ViewBag.s = "Email Exists. Sign In instead";
                return RedirectToAction("Login");
            }
            else
            {
                //if email is new
                int key = isAdd[1];
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7), // Set the expiration date of the cookie
                    HttpOnly = true // Specify if the cookie is accessible only through HTTP
                };
               
                HttpContext.Response.Cookies.Append($"{key.ToString()}", System.DateTime.Now.ToString(),cookieOptions);
                //TempData["Alert"] = "Welcome to new User, Sign Up Successfully !! ";
                object data = "Account Created";
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
