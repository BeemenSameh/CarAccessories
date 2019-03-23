using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    [Authorize(Roles = "Vendor")]
    public class AddProductToVendorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: VendorProduct
        public ActionResult Index()
        {
            var prod = db.Products.ToList();
            return View(prod);
        }
        public ActionResult Addprice(int id)
        {
            var vprice = db.Products.FirstOrDefault(v => v.ID == id);
            VendorProduct vendorproduct = new VendorProduct();
            vendorproduct.Product = vprice;
            return View(vendorproduct);
        }
        [HttpPost]
        public ActionResult save(VendorProduct vendor, int id)
        {
            Product product = db.Products.FirstOrDefault(p => p.ID == id);
            VendorProduct vendorproduct = new VendorProduct();

            bool claimIdentity = User.Identity is ClaimsIdentity;
            Vendor user = new Vendor();
            List<OrderDetails> orders = new List<OrderDetails>();
            if (claimIdentity)
            {
                ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    user = db.Vendors.Where(i => i.ID == userIdValue).FirstOrDefault();
                }
            }
            vendorproduct.Product = product;
            vendorproduct.Price = vendor.Price;
            vendorproduct.Sale_price = vendor.Sale_price;
            vendorproduct.Quantity = vendor.Quantity;
            vendorproduct.Vendor = user;
            db.VendorProducts.Add(vendorproduct);
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}