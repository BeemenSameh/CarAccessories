using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarAccessories.Models;
using CarAccessories.Models.ViewModel;
//using CarAccessories.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class SaleController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Sale
        public ActionResult Index()
        {

            BrandsProducts b = new BrandsProducts
            {
                Brands = db.Brands.ToList(),
                VendorProducts = db.VendorProducts.Where(p=>p.Sale_price!=0).ToList(),
                CategoriesList = db.Categories.ToList()
            };
            foreach (var vendorProduct in b.VendorProducts)
            {
                db.Entry(vendorProduct).Reference(p => p.Product).Load();
                db.Entry(vendorProduct).Reference(v => v.Vendor).Load();
            }
           
            return View(b);
        }

        //public ActionResult GetProductsByCatId(int Category_ID)
        //{
        //    List<Product> ProductsList = db.Products.Where(i => i.Category.ID == Category_ID).Where(p=>p.Sale_price!=0).ToList();
        //    return PartialView("_GetProdByCatIdPartialView", ProductsList);

        //}

        public ActionResult GetAllProducts()
        {
            List<VendorProduct> AllVendorProductsList = db.VendorProducts.Where(p=>p.Sale_price!=0).ToList();
            foreach (var i in AllVendorProductsList)
            {
                db.Entry(i).Reference(p => p.Product).Load();
                db.Entry(i).Reference(p => p.Vendor).Load();
            }
            return PartialView("_AllProductsPartialView", AllVendorProductsList);
        }
    }
}