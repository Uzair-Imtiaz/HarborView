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
            var userLocation = HttpContext.Session.GetString("UserLocation");
            WebProjectAuthenticateUserContext context = new WebProjectAuthenticateUserContext();
            List<FoodItems> foodItems = context.Fooditems.Where(e => e.Location == userLocation).ToList();
            if (foodItems.Count == 0)
            {
                FoodItems f1 = new FoodItems();
                FoodItems f2 = new FoodItems();
                FoodItems f3 = new FoodItems();
                FoodItems f4 = new FoodItems();

                f1.Description = "This pepperoni pan pizza is made with a simple yet superlative from - scratch tomato sauce, two types of mozzarella, Parmesan cheese, pepperoni, and either store-bought or homemade dough.";
                f2.Description = "Lasagne are a type of pasta, possibly one of the oldest types, made of very wide, flat sheets. Either term can also refer to an Italian dish made of stacked layers of lasagne alternating with fillings.";
                f3.Description = "Sushi, a staple rice dish of Japanese cuisine, consisting of cooked rice flavoured with vinegar and a variety of vegetable, egg, or raw seafood garnishes and served cold.";
                f4.Description = "A taco is a traditional Mexican dish consisting of a small hand - sized corn or wheat tortilla topped with a filling. The tortilla is then folded around the filling and eaten by hand.";
                
                f1.Image = "https://sipbitego.com/wp-content/uploads/2021/08/Pepperoni-Pizza-Recipe-Sip-Bite-Go.jpg";
                f2.Image = "https://media.istockphoto.com/photos/lasagna-on-a-square-white-plate-picture-id535851351?k=20&m=535851351&s=612x612&w=0&h=jdWOk9G_OOzHvPrvFrigqzk3EoucmIhUZr1-ey9NcGM=";
                f3.Image = "https://www.curiouscuisiniere.com/wp-content/uploads/2013/06/Japanese-Sushi-0458.450-450x270.jpg";
                f4.Image = "https://lp-cms-production.imgix.net/image_browser/tacos_mexico_G.jpg";


                f1.Name = "Pepporoni Pizza";
                f2.Name = "Lasagne";
                f3.Name = "Sushi";
                f4.Name = "Mexican Tacos";

                foodItems.Add(f1);
                foodItems.Add(f2);
                foodItems.Add(f3);
                foodItems.Add(f4);
            }
            return View(foodItems);
        }
        public IActionResult reservation()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Cook"))
            {
                TempData["signinFromMembership"] = "You need to login first!";
                return RedirectToAction("Login", "Authentication");
            }
            return View();
        }

        [HttpPost]
        public IActionResult reservation(string Date,string Time,string Message,string Name,string PhoneNo)
        {

            string pack = Request.Form["package"];
            string adult = Request.Form["adults"];
            string child = Request.Form["children"];
            string infants = Request.Form["infants"];

            if(Date==null || Time==null || Name==null || PhoneNo==null || adult == null)
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
            DateTime dt = new DateTime(int.Parse(da[0]), int.Parse(da[1]), int.Parse(da[2]), int.Parse(ta[0]), int.Parse(ta[1]), 0);
            din.Date = dt;

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