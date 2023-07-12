using HarborView_Inn.Models;
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
            if (!HttpContext.Request.Cookies.ContainsKey("Cook"))
            {
                TempData["signinFromMembership"] = "You need to login first!";
                return RedirectToAction("Login", "Authentication");
            }
            ViewBag.cat = Request.Query["category"].ToString();
            WebProjectAuthenticateUserContext context = new WebProjectAuthenticateUserContext();
            var vis = context.Reservation.Where(e => e.isActive == true).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Confirmation(string CheckIn, string CheckOut, string Message, string Name, string PhoneNo)
        {
            ViewBag.em = Request.Cookies["Cook"];
            ViewBag.cat = Request.Query["category"].ToString();

            string pack = Request.Form["package"];
            string catg = Request.Form["category"];
            string adult = Request.Form["adults"];
            string child = Request.Form["children"];
            string infants = Request.Form["infants"];
            string rooms = Request.Form["rooms"];

            if (string.IsNullOrEmpty(CheckIn) || string.IsNullOrEmpty(CheckOut) ||
                string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(PhoneNo) ||
                string.IsNullOrEmpty(adult))
            {
                ViewBag.allReq = "Fill in all the required fields!";
                return View();
            }

            // Perform additional validations if needed

            // Create a new Reservation object and set its properties
            Reservation res = new Reservation();
            res.Category = catg;
            res.CheckIn = DateTime.Parse(CheckIn);
            res.CheckOut = DateTime.Parse(CheckOut);
            res.noOfRooms = int.Parse(rooms);
            res.status = "Pending";
            res.ReservationName = Name;

            if (res.CheckOut < res.CheckIn)
            {
                ViewBag.invalidDates = "Check-out date cannot be earlier than Check-in date.";
                return View();
            }

            TimeSpan difference = res.CheckOut.Subtract(res.CheckIn);

            int adultCount = int.TryParse(child, out int adultValue) ? adultValue : 0;
            int childCount = int.TryParse(child, out int childValue) ? childValue : 0;
            int InfantCount = int.TryParse(infants, out int infantValue) ? infantValue : 0;

            if (catg == "Silver")
            {
                res.Bill = (float)(childCount + adultCount + InfantCount / 2 / 2) * difference.Days * 1000;
            }
            else if (catg == "General")
            {
                res.Bill = (float)(childCount + adultCount + InfantCount / 2 / 2) * difference.Days * 1500;
            }
            else if (catg == "gold")
            {
                res.Bill = (float)(childCount + adultCount + InfantCount / 2 / 2) * difference.Days * 3000;
            }
            else if (catg == "Platinum")
            {
                res.Bill = (float)(childCount + adultCount + InfantCount / 2 / 2) * difference.Days * 4500;
            }

            res.Email = Request.Cookies["Cook"];
            res.isActive = true;

            // Save the reservation to the database
            using (WebProjectAuthenticateUserContext context = new WebProjectAuthenticateUserContext())
            {
                // Add the reservation to the context
                context.Reservation.Add(res);
                context.SaveChanges();
            }

            TempData["Book"] = "Your reservation request has been received!";
            return RedirectToAction("Index", "Home");
        }


    }
}
