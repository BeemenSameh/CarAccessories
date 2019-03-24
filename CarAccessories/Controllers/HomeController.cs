using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarAccessories.Models.ViewModel;

namespace CarAccessories.Controllers
{
    //[Authorize(Roles = "Customer"),AllowAnonymous]
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            BrandsProducts b = new BrandsProducts
            {
                Brands = db.Brands.ToList(),
                VendorProducts = db.VendorProducts.ToList(),

            };
            foreach (var vendorProduct in b.VendorProducts)
            {
                db.Entry(vendorProduct).Reference(p => p.Product).Load();
                db.Entry(vendorProduct).Reference(v => v.Vendor).Load();
            }

            return View(b);
        }
        public ActionResult ShowThisBrandModels(int BrandId)
        {
            BrandModelsAndBrandProducts b = new BrandModelsAndBrandProducts
            {
                ModelsList = db.Models.Where(m => m.Brand.ID == BrandId).ToList(),
                VendorProductList = db.VendorProducts.Where(p => p.Product.Model.Brand.ID == BrandId).ToList(),
                CategoriesList = db.Categories.ToList(),
                BrandId= BrandId
            };
            foreach (var VendorProductList in b.VendorProductList)
            {
                db.Entry(VendorProductList).Reference(p => p.Product).Load();
                db.Entry(VendorProductList.Product).Reference(m => m.Model).Load();
                db.Entry(VendorProductList.Product.Model).Reference(br => br.Brand).Load();
                db.Entry(VendorProductList).Reference(p => p.Vendor).Load();
            }
            foreach(var m in b.ModelsList)
            {
                db.Entry(m).Reference(br => br.Brand).Load();
            }
            return View(b);
        }

       



        public ActionResult GetAllProducts(int BrandId)
        {
            List<VendorProduct> BrandVendorProductsList = db.VendorProducts.Where(p => p.Product.Model.Brand.ID == BrandId).ToList();
            foreach (var i in BrandVendorProductsList)
            {
                db.Entry(i).Reference(p => p.Product).Load();
                db.Entry(i).Reference(p => p.Vendor).Load();
            }
            return PartialView("_AllProductsPartialView", BrandVendorProductsList);
        }

        //public ActionResult GetAllProducts()
        //{
        //    List<Product> ProductList = db.Products.ToList();
        //    return PartialView("_AllProductsPartialView", ProductList);
        //}

        

        public ActionResult ShowProductDetails(int id)
        {

            VendorProduct p = db.VendorProducts.Where(i => i.ID == id).FirstOrDefault();
            db.Entry(p).Reference(b => b.Product).Load();
            db.Entry(p).Reference(v => v.Vendor).Load();
            db.Entry(p.Product).Reference(m => m.Model).Load();
            db.Entry(p.Product.Model).Reference(b => b.Brand).Load();
            return View("_ProductDetailsView", p);
        }

        public ActionResult ShowVendorDetails(int id)
        {
            return View();
        }

    }
}