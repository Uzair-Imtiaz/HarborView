using Microsoft.AspNetCore.Mvc;

namespace HarborView_Inn.Controllers
{
    public class Food : Controller
    {
        public IActionResult Dining()
        {
            return View();
        }
        public IActionResult reservation()
        {
            return View();
        }
    }
}
