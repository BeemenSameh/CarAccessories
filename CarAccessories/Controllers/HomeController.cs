using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarAccessories.Models.ViewModel;

namespace CarAccessories.Controllers
{
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
                CategoriesList = db.Categories.ToList()
            };
            foreach (var VendorProductList in b.VendorProductList)
            {
                db.Entry(VendorProductList).Reference(p => p.Product).Load();
                db.Entry(VendorProductList.Product).Reference(m => m.Model).Load();
                db.Entry(VendorProductList.Product.Model).Reference(br => br.Brand).Load();
                db.Entry(VendorProductList).Reference(p => p.Vendor).Load();
            }
            return View(b);
        }

        public ActionResult GetProductsByModelId(int Model_ID)
        {
            //{  ? p.Price == f : p.Sale_price == f}
            List<VendorProduct> VendorProductList = db.VendorProducts.Where(i => i.Product.Model.ID == Model_ID).ToList();
            foreach (var v in VendorProductList)
            {
                db.Entry(v).Reference(p => p.Product).Load();
                db.Entry(v).Reference(p => p.Vendor).Load();
            }

            return PartialView("_GetProdByCatIdPartialView", VendorProductList);

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

        public ActionResult GetProductsByCategoryId(int Cat_ID)
        {
            List<Product> ProductsList = db.Categories.Where(i => i.ID == Cat_ID).Select(p => p.Products).FirstOrDefault().ToList();
            List<VendorProduct> VendorProductList = new List<VendorProduct>();
            foreach (var pl in ProductsList)
            {
                VendorProductList.AddRange(pl.VendorProducts);
            }
            return PartialView("_GetProdByCatIdPartialView", VendorProductList);
        }

        public ActionResult ShowProductDetails(int id)
        {

            VendorProduct p = db.VendorProducts.Where(i => i.ID == id).FirstOrDefault();
            db.Entry(p).Reference(b => b.Product).Load();
            db.Entry(p).Reference(v => v.Vendor).Load();
            db.Entry(p.Product).Reference(m => m.Model).Load();
            db.Entry(p.Product.Model).Reference(b => b.Brand).Load();
            return View("_ProductDetailsView", p);
        }

        //public ActionResult ShowVendorDetails(int id)
        //{

        //}

    }
}