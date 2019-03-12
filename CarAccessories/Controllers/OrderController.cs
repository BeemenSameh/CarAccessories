using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    public class OrderController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        {
            bool claimIdentity = User.Identity is ClaimsIdentity;
            ApplicationUser user = new ApplicationUser();
            List<OrderDetails> orders = new List<OrderDetails>();

            if (claimIdentity)
            {
                ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    user = context.Users.Where(i => i.Id == userIdValue).FirstOrDefault();
                    context.Entry(user).Collection(i => i.Order).Load();
                    foreach (var i in user.Order)
                    {
                        context.Entry(i).Collection(c => c.OrderDetails).Load();
                        foreach (var OrderDetails in i.OrderDetails)
                        {
                            context.Entry(OrderDetails).Reference(c => c.VendorProduct).Load();
                            context.Entry(OrderDetails.VendorProduct).Reference(c => c.Product).Load();
                        }
                    }
                    orders.AddRange(context.OrderDetails.Where(c => c.Order.Isbuy == false));
                }
            }
            return View(orders);
        }
        [HttpPost]
        public ActionResult update(int[] num_product1, int totalprice)
        {
            ApplicationUser user = new ApplicationUser();
            
            bool claimIdentity = User.Identity is ClaimsIdentity;
            if (claimIdentity)
            {
                ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    user = context.Users.Where(i => i.Id == userIdValue).FirstOrDefault();
                    context.Entry(user).Collection(i => i.Order).Load();
                    
                }
            }
            Order c = user.Order.FirstOrDefault(g => g.Isbuy == false);
            context.Entry(c).Reference(i => i.Customer).Load();
            List<OrderDetails> d = c.OrderDetails.ToList();
            if (c.Customer.money > c.TotalPrice)
            {
                c.TotalPrice = totalprice;
                context.Entry(c).Collection(o => o.OrderDetails).Load();
                for (int i = 0; i < num_product1.Length; i++)
                {
                    d[i].Quantity = num_product1[i];
                }
                c.Isbuy = true;
                context.SaveChanges();
                return RedirectToAction("Index","Shop");
            }
            else
            {
                return RedirectToAction("Index");

            }
            }
        }


    }







