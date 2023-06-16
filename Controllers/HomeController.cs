using HarborView_Inn.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HarborView_Inn.Controllers
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
            //ViewBag.Title = "HarborView Inn";
            ViewBag.em = Request.Cookies["Cook"];
            return View();
        }

        public IActionResult Locations()
        {
            //ViewBag.title = "HarborView Inn | About";
            ViewBag.em = Request.Cookies["Cook"];
            return View();
        }

        public IActionResult About()
        {
            //ViewBag.title = "HarborView Inn | About";
            ViewBag.em = Request.Cookies["Cook"];
            return View();
        }
        public IActionResult Chat()
        {
            //ViewBag.Title = "HarborView Inn";
            ViewBag.em = Request.Cookies["Cook"];
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}