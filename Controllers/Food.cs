using HarborView_Inn.Models;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Diagnostics.Tracing;

namespace HarborView_Inn.Controllers
{
    public class Food : Controller
    {
       
        public IActionResult Dining()
        {
            ViewBag.em = Request.Cookies["Cook"];
            if (HttpContext.Request.Cookies.ContainsKey("Cook1") || HttpContext.Request.Cookies.ContainsKey("Cook2"))
            {
                WebProjectAuthenticateUserContext context = new WebProjectAuthenticateUserContext();
                FoodItems food = new FoodItems();
                var f = context.Fooditems.Where(e => e.Name == Request.Cookies["Cook1"]).ToList();
                if (f.Count==0)
                {
                    f = context.Fooditems.Where(e => e.Name == Request.Cookies["Cook2"]).ToList();
                }
                food = f[0];
                var desc = food.Description.Split('@');
                ViewBag.desc1 = desc[0];
                ViewBag.desc2 = desc[1];
                ViewBag.desc3 = desc[2];
                ViewBag.desc4 = desc[3];

                var path = food.Image.Split('@');
                ViewBag.path1 = path[0];
                ViewBag.path2 = path[1];
                ViewBag.path3 = path[2];
                ViewBag.path4 = path[3];

                var iname = food.itemName.Split('@');
                ViewBag.iname1 = iname[0];
                ViewBag.iname2 = iname[1];
                ViewBag.iname3 = iname[2];
                ViewBag.iname4 = iname[3];


            }
            

            return View();
        }
        public IActionResult reservation()
        {
            ViewBag.em = Request.Cookies["Cook"];
            return View();
        }

        [HttpPost]
        public IActionResult reservation(string Date,string Time,string Message,string Name,string PhoneNo)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Cook"))
            {
                ViewBag.inv = "Login/Signup First !!";
                return View();
            }

            string pack = Request.Form["package"];
            string adult = Request.Form["adults"];
            string child = Request.Form["childrens"];
            string infants = Request.Form["infants"];

            if(Date==null || Time==null || Name==null || PhoneNo==null || adult == null || child==null)
            {
                ViewBag.r = "You Left One of the Field Empty !!";
                return View();
            }

            WebProjectAuthenticateUserContext context = new WebProjectAuthenticateUserContext();

            // get all records to see if booking has done and make its isActive 0 if 3 hours has passes
            var vis = context.DiningTable.Where(e => e.isActive == true).ToList();
           
            foreach (var st in vis)
            {
				var da1 = st.Date.ToString().Split(' ');
                var da2 = DateTime.Now.ToString().Split(' ');
				if (st.Date > DateTime.Now && da1[0] == da2[0])
                {
                    st.isActive = false;
                    context.SaveChanges();
                }

            }

           
            DiningTable din=new DiningTable();
            var da = Date.Split('-');
            var ta = Time.Split(':');
            //var orig = int.Parse(ta[0]);
            //if (int.Parse(ta[0]) > 12)
            //{
            //    orig = int.Parse(ta[0]) - 12;
            //}
            DateTime dt = new DateTime(int.Parse(da[0]), int.Parse(da[1]), int.Parse(da[2]), int.Parse(ta[0]), int.Parse(ta[1]), 0);
            din.Date = dt;
            din.noOfGuest = int.Parse(adult) + int.Parse(child);
            din.Bill = din.noOfGuest * 200;

            din.Email =Request.Cookies["Cook"]; 
            din.isActive = true;
            din.category = pack;

            // lets say we have 3 tables only to fit people  . 
            bool isGiven = false;
            if (vis.Count >=3)
            {
                foreach (var st in vis)
                {
                    var b = dt.ToString().Split(' ');
                    var a = st.Date.ToString().Split(' ');
                    if (dt > st.Date.AddHours(3) && a[0] == b[0] || dt.AddHours(3) <st.Date && a[0] == b[0])
                    {
                        din.bookedTable = st.TableId;  // if table is booked at 12pm and all tables are reserved then give this table for booking at 9 am or 3pm and record table id . 
                        isGiven = true;
                        break;
                    }
                }
            }

            if (!isGiven && vis.Count<3 || isGiven)
            {
                context.DiningTable.Add(din);
                context.SaveChanges();
                ViewBag.book = "Your Table has been Booked !! ";
            }
            else
            {
                ViewBag.book = "No Free Space at the moment !!";
            }


            
            ViewBag.em= Request.Cookies["Cook"];

           

            

            return View();
        }

    }
}