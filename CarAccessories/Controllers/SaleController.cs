using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarAccessories.Models;
//using CarAccessories.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class SaleController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Sale
        public ActionResult Index()
        {

            List<Product> SaleProductsList = db.Products.Where(i => i.Sale_price != 0).ToList();
              
            return View(SaleProductsList);
        }

        //public ActionResult GetProductsByCatId(int Category_ID)
        //{
        //    List<Product> ProductsList = db.Products.Where(i => i.Category.ID == Category_ID).Where(p=>p.Sale_price!=0).ToList();
        //    return PartialView("_GetProdByCatIdPartialView", ProductsList);

        //}

        public ActionResult GetAllProducts()
        {
            List<Product> ProductList = db.Products.Where(p=>p.Sale_price!=0).ToList();
            return PartialView("_AllProductsPartialView", ProductList);
        }
    }
}