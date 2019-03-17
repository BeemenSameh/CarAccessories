using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    public class VendorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: vendor
        public ActionResult Index(string id)
        {
            var user = db.Users.FirstOrDefault(use => use.Id == id);
            db.Entry(user).Reference(vendor => vendor.Vendor).Load();
            return View(user);
        }

        // GET: vendor/Details/5
        public ActionResult Details(string id)
        {
            var user = db.Users.FirstOrDefault(use => use.Id == id);
            db.Entry(user).Reference(vendor => vendor.Vendor).Load();
            return PartialView("_VendorDetails",user);
        }

        // GET: vendor/Create
        
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
