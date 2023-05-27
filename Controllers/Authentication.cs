using Microsoft.AspNetCore.Mvc;

namespace HarborView_Inn.Controllers
{
    public class Authentication : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string user)
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string user)
        {
            return View();
        }
    }
}
