using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    public class AddProductToVendorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext(); 
        // GET: VendorProduct
        public ActionResult Index()
        {
            var prod = db.Products.ToList();
            return View(prod);
        }
    }
}