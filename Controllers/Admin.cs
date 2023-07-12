using Microsoft.AspNetCore.Mvc;
using HarborView_Inn.Models;

namespace HarborView_Inn.Controllers
{
    [Route("Admin")]
    public class Admin : Controller
    {
        public IActionResult admin()
        {
            if (HttpContext.Session.GetString("admin") == "ok")
            {
                using WebProjectAuthenticateUserContext context = new WebProjectAuthenticateUserContext();
                List<Reservation> pendingReservations = context.Reservation
                    .Where(e => e.status == "Pending")
                    .ToList();
                List<DiningTable> tables = context.DiningTable
                    .Where(e => e.status == "Pending")
                    .ToList();

                AdminViewModel model = new AdminViewModel();
                model.room = pendingReservations;
                model.dining = tables;
                
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost("/Admin/acceptReservation/{ResId}")]
        public IActionResult acceptReservation(int ResId)
        {
            // Retrieve the reservation from the database using the reservationId
            using WebProjectAuthenticateUserContext context = new WebProjectAuthenticateUserContext();
            Reservation reservation = context.Reservation.Find(ResId);

            if (reservation != null)
            {
                // Update the reservation status to "Accepted"
                reservation.status = "Accepted";
                context.SaveChanges();
            }
            return RedirectToAction("admin");
        }

        [HttpPost("/Admin/rejectReservation/{ResId}")]
        public IActionResult rejectReservation(int ResId)
        {
            using WebProjectAuthenticateUserContext context = new WebProjectAuthenticateUserContext();
            var reservation = context.Reservation.Find(ResId);

            if (reservation != null)
            {
                reservation.status = "Rejected";
                context.SaveChanges();
            }

            return RedirectToAction("admin");
        }

        [HttpPost("/Admin/acceptReservation/{ResId}")]
        public IActionResult acceptBooking(int ResId)
        {
            // Retrieve the reservation from the database using the reservationId
            using WebProjectAuthenticateUserContext context = new WebProjectAuthenticateUserContext();
            var reservation = context.DiningTable.Find(ResId);

            if (reservation != null)
            {
                // Update the reservation status to "Accepted"
                reservation.status = "Accepted";
                context.SaveChanges();
            }
            return RedirectToAction("admin");
        }

        [HttpPost("/Admin/rejectReservation/{ResId}")]
        public IActionResult rejectBooking(int ResId)
        {
            using WebProjectAuthenticateUserContext context = new WebProjectAuthenticateUserContext();
            var reservation = context.DiningTable.Find(ResId);

            if (reservation != null)
            {
                reservation.status = "Rejected";
                context.SaveChanges();
            }

            return RedirectToAction("admin");
        }

    }
}
