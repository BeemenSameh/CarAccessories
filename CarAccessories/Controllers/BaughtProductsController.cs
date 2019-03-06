using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    public class BaughtProductsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        List<Product> ProductList = new List<Product>();
        // GET: BaughtProducts
        public ActionResult Index()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;

                    ApplicationUser user = db.Users.Where(i => i.Id == userIdValue).FirstOrDefault();
                    db.Entry(user).Collection(c => c.Cart).Load();
                    foreach (var i in user.Cart)
                    {
                       // db.Entry(i).Reference(c => c.Orde).Load();



                    }
                    return View(user);
                }
                else
                {
                    return Redirect("/Account/Login");

                }


            }
            else
            {
                return Redirect("/Account/Register");
            }

        }
          
    }
}