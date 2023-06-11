using Microsoft.AspNetCore.Mvc;

namespace HarborView_Inn.Controllers
{
    public class Membership : Controller
    {
        public IActionResult get_membership()
        {
            return View();
        }

        public IActionResult confirmation()
        {

            ViewBag.cat = Request.Query["category"].ToString();
            return View();
        }
    }
}
