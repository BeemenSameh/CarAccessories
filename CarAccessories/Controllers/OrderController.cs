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
            ApplicationUser user = new ApplicationUser();
            List<OrderDetails> orders = new List<OrderDetails>();
       
            if (User.Identity is ClaimsIdentity claimsIdentity)
            {
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
        public ActionResult update(int[] num_product1)
        {
            ApplicationUser user = new ApplicationUser();
            if (User.Identity is ClaimsIdentity claimsIdentity)
            {
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    user = context.Users.Where(i => i.Id == userIdValue).FirstOrDefault();
                    context.Entry(user).Collection(i => i.Order).Load();
                }
            }
            List<Order> c = user.Order.ToList();
            for (int i = 0; i < num_product1.Length; i++)
            {
                foreach (var item in c[i].OrderDetails)
                {
                    item.Quantity = num_product1[i];
                }
                
            }
            c.ForEach(cart => cart.Isbuy = true);
            context.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}






