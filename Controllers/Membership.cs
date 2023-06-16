using HarborView_Inn.Models;
using Microsoft.AspNetCore.Mvc;

namespace HarborView_Inn.Controllers
{
    public class Membership : Controller
    {
        public IActionResult get_membership()
        {
            ViewBag.em = Request.Cookies["Cook"];
            return View();
        }

        public IActionResult confirmation()
        {
            ViewBag.em = Request.Cookies["Cook"];
            ViewBag.cat = Request.Query["category"].ToString();
            WebProjectAuthenticateUserContext context = new WebProjectAuthenticateUserContext();
            var vis = context.Reservation.Where(e => e.isActive == true).ToList();
            //int rooms = 0;
            //foreach (var s in vis)
            //{
            //    rooms += s.noOfRooms;
            //}
            ViewBag.rem = $"{3 - vis.Count} Rooms are left !! Hurry Up !!";

            return View();
        }
        [HttpPost]
        public IActionResult confirmation(string CheckIn,string CheckOut, string Message, string Name, string PhoneNo)
        {
            ViewBag.em = Request.Cookies["Cook"];
            ViewBag.cat = Request.Query["category"].ToString();

            if (!HttpContext.Request.Cookies.ContainsKey("Cook"))
            {
                ViewBag.inv = "Login/Signup First !!";
                return View();
            }
            
            string pack = Request.Form["package"];
            string catg = Request.Form["category"];
            string adult = Request.Form["adults"];
            string child = Request.Form["childrens"];
            string infants = Request.Form["infants"];
            if (CheckIn == null || CheckOut==null || Name == null || PhoneNo == null || adult == null || child == null)
            {
                ViewBag.r = "You Left One of the Field Empty !!";
                return View();
            }

            WebProjectAuthenticateUserContext context = new WebProjectAuthenticateUserContext();

            // get all records to see if booking has done and make its isActive 0 if checkout date has passed
            var vis = context.Reservation.Where(e => e.isActive == true).ToList();
            //int rooms = 0;
            //foreach (var s in vis)
            //{
            //    rooms += s.noOfRooms;
            //}
            foreach (var st in vis)
            {
				var da1 = st.CheckOut.ToString().Split(' ');
				var da2 = DateTime.Now.ToString().Split(' ');
				if (st.CheckOut >  DateTime.Now && da1[0] == da2[0])
                {
                    st.isActive = false;
                    context.SaveChanges();
                }

            }

            Reservation res=new Reservation();
            res.noOfRooms = (int.Parse(child) + int.Parse(adult))/2;
            //if(res.noOfRooms + rooms >10) // cant book room
            //{
            //    ViewBag.book = "No Free Space at the moment !!";
            //    return View();
            //}
            res.Category = catg;
            var ci = CheckIn.Split('-');
            DateTime checkIn = new DateTime(int.Parse(ci[0]), int.Parse(ci[1]), int.Parse(ci[2]), 12, 0, 0);
            var co = CheckOut.Split('-');
            DateTime checkOut = new DateTime(int.Parse(co[0]), int.Parse(co[1]), int.Parse(co[2]), 12, 0, 0);
            res.CheckOut = checkOut;
            res.CheckIn = checkIn;
            if (res.CheckOut < res.CheckIn)
            {
                ViewBag.err = "Checkin & Checkout Dates Mismatched !!";
                return View();
            }
            TimeSpan difference = res.CheckOut.Subtract(res.CheckIn);
            if (catg=="Silver")
            {
                res.Bill = res.noOfRooms * difference.Days * 1000;
            }
            else if (catg == "General")
            {
                res.Bill = res.noOfRooms * difference.Days * 1500;
            }
            else if (catg == "gold")
            {
                res.Bill = res.noOfRooms * difference.Days * 3000;
            }
            else //(catg == "Platinum")
            {
                res.Bill = res.noOfRooms * difference.Days * 4500;
            }
            res.Email= Request.Cookies["Cook"];
            res.isActive = true;

            // lets say we have 10 rooms only to fit people  . 
            bool isGiven = false;
            if (vis.Count >=3)  // no of enttries 3 hngi to hi phr woh exisiting mn dhundenga
            {
                foreach (var st in vis)
                {
                    var b = checkIn.ToString().Split(' ');
                    var a = st.CheckOut.ToString().Split(' ');
                    if (checkIn > st.CheckOut && /*a[0] == b[0] &&*/ catg==st.Category || checkOut < st.CheckIn && /*a[0] == b[0] &&*/ catg == st.Category)
                    {
                        res.bookedRoom = st.ResId; 
                        isGiven = true;
                        break;
                    }
                }
            }

            if (!isGiven && vis.Count < 3 || isGiven)
            {
                context.Reservation.Add(res);
                context.SaveChanges();
               
                var vis1 = context.Reservation.Where(e => e.isActive == true).ToList();
                ViewBag.rem = $"{3 - vis1.Count} Rooms are left !! Hurry Up !!";
                ViewBag.book = "Your Room has been Booked !! ";
            }
            else
            {
                var vis1 = context.Reservation.Where(e => e.isActive == true).ToList();
                ViewBag.rem = $"{3 - vis1.Count} Rooms are left !! Hurry Up !!";
               
                ViewBag.book = "No Free Space at the moment !!";
            }
            return View();
        }


    }
}
