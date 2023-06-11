using Microsoft.AspNetCore.Mvc;

namespace HarborView_Inn.Controllers
{
    public class Admin : Controller
    {
        public IActionResult admin()
        {
            return View();
        }
    }
}
