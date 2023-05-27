using Microsoft.AspNetCore.Mvc;

namespace HarborView_Inn.Controllers
{
    public class Booking : Controller
    {
        public IActionResult Book()
        {
            return View();
        }
    }
}
