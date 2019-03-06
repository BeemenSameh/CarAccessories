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
                    context.Entry(user).Collection(i => i.Cart).Load();
                    foreach (var i in user.Cart)
                    {
                        context.Entry(i).Reference(c => c.OrderDetails).Load();
                    }
                    orders.AddRange(context.OrderDetails.Where(c => c.Order.Isbuy == false));
                }
            }
            return View(orders);
        }
    }
}