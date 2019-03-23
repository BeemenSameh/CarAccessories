using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProductController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminProduct
        public ActionResult Index()
        {
            var products = db.Products.ToList();
            return View(products);
        }
        public ActionResult GetAllProduct()
        {
            var products = db.Products.ToList();
            return PartialView("_GetAllProduct",products);
        }
        public ActionResult DeleteProduct(int id)
        {
            var prod = db.Products.FirstOrDefault(prd => prd.ID == id);
            return View(prod);
        }
        public ActionResult ConfirmDeleteProduct(int id)
        {
            var prod = db.Products.FirstOrDefault(prd => prd.ID == id);
            db.Products.Remove(prod);
            db.SaveChanges();
            return RedirectToAction("GetAllProduct");
        }
    }
}