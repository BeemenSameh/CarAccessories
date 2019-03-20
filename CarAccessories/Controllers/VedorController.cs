using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    public class VedorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Vedor
        public ActionResult Index(string id)
        {
            var user = db.Users.FirstOrDefault(use => use.Id == id);
            db.Entry(user).Reference(vendor => vendor.Vendor).Load();
            return View(user);
        }

        // GET: Vedor/Details/5
        public ActionResult Details(string id)
        {
            var count = 0;
            var vend = db.Vendors.Where(v=>v.ID==id).FirstOrDefault();
            foreach (var item in vend.VendorProduct)
            {
                count += db.Entry(item).Collection(ord => ord.OrderDetails).;
            }
            //var orders = db.OrderDetails.
            var user = db.Users.FirstOrDefault(use => use.Id == id);
            db.Entry(user).Reference(vendor => vendor.Vendor).Load();
            foreach (var VP in user.Vendor.VendorProduct)
            {
                foreach (var ord in VP.OrderDetails)
                {
                    db.Entry(ord).Reference(o => o.Order).Load();
                }
            }
            return PartialView("_VendorDetails",user);
        }

        // GET: Vedor/Create
        
        //Get Products
        public ActionResult GetAllProducts(string id)
        {
            var user = db.Users.FirstOrDefault(use => use.Id == id);
            db.Entry(user).Reference(vendor => vendor.Vendor).Load();
            db.Entry(user.Vendor).Collection(prod => prod.VendorProduct).Load();
            foreach (var VP in user.Vendor.VendorProduct)
            {
                db.Entry(VP).Reference(prod => prod.Product).Load();
                db.Entry(VP.Product).Reference(cat => cat.Category).Load();
            }
            return View(user);
        }
    }
}
