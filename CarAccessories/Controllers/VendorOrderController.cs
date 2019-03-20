using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    public class VendorOrderController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: VendorOrder
        public ActionResult Index()
        {

            ICollection<OrderDetails> ordetail = new List<OrderDetails>();
            var user = new Vendor();
            bool claimIdentity = User.Identity is ClaimsIdentity;
            if (claimIdentity)
            {
                ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    user = db.Vendors.Where(i => i.ID == userIdValue).FirstOrDefault();
                            db.Entry(user).Collection(prod => prod.VendorProduct).Load();
                    foreach (var VP in user.VendorProduct)
                    {
                        db.Entry(VP).Reference(prod => prod.Product).Load();
                        db.Entry(VP.Product).Reference(cat => cat.Category).Load();
                    }
                }
            }
            return PartialView("_VendorOrders",user);
        }

        public ActionResult AddModel()
        {
            var Brands = db.Brands.ToList();
            ViewBag.Brand = Brands;
            return PartialView("_AddModel",new Model());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddModel(Model Model)
        {
            if (ModelState.IsValid==true)
            {
                return RedirectToAction("addProduct", "VendorProduct");
            }
            return PartialView("_AddModel",Model);
        }

        public ActionResult AddBrand()
        {
            return PartialView("_AddBrand", new Brand());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBrand(Brand Brand)
        {
            if (ModelState.IsValid == true)
            {
                db.Brands.Add(Brand);
                db.SaveChanges();
                return RedirectToAction("addModel");
            }
            return PartialView("_AddBrand", Brand);
        }
    }
}