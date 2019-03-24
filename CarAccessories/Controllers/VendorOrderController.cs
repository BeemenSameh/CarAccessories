using CarAccessories.Models;
using CarAccessories.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    [Authorize]
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
            ModelBrandVM modelbrandVM = new ModelBrandVM()
            {
                Brands = db.Brands.ToList()
            };
            return PartialView("_AddModel", modelbrandVM);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddModel(ModelBrandVM modelbrandVM)
        {
            if (ModelState.IsValid==true)
            {
                var br = db.Brands.Where(b => b.ID == modelbrandVM.Brandid).FirstOrDefault();

                var Model = new Model()
                {
                    ModelName = modelbrandVM.ModelName,
                    Year = modelbrandVM.Year,
                    Brand = br
                };

                // db.Entry(modelbrandVM).Collection(m=>m.Brands).Load();
                db.Models.Add(Model);
                db.SaveChanges();
               
            }

            return RedirectToAction("GetProducts", "VendorProduct");
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
                return RedirectToAction("GetProducts", "VendorProduct");
            }
            return PartialView("_AddBrand", Brand);
        }
    }
}