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
                VendorProducts = db.VendorProducts.Where(p=>p.Sale_price!= 0).Where(p=>p.Sale_price!= null).ToList(),
                CategoriesList = db.Categories.ToList()
            };
            foreach (var vendorProduct in b.VendorProducts)
            {
                db.Entry(vendorProduct).Reference(p => p.Product).Load();
                db.Entry(vendorProduct).Reference(v => v.Vendor).Load();
            }
           
            return View(b);
        }

        public ActionResult GetAllProducts()
        {
            List<VendorProduct> AllVendorProductsList = db.VendorProducts.Where(p => p.Sale_price != 0).Where(p => p.Sale_price != null).ToList();
            foreach (var i in AllVendorProductsList)
            {
                db.Entry(i).Reference(p => p.Product).Load();
                db.Entry(i).Reference(p => p.Vendor).Load();
            }
            return PartialView("_AllProductsPartialView", AllVendorProductsList);
           
        }

        public ActionResult GetProductsByModelId(int Model_ID)
        {
            //{  ? p.Price == f : p.Sale_price == f}
            List<VendorProduct> VendorProductList = db.VendorProducts.Where(i => i.Product.Model.ID == Model_ID).Where(p => p.Sale_price != 0).Where(p => p.Sale_price != null).ToList();
            foreach (var v in VendorProductList)
            {
                db.Entry(v).Reference(p => p.Product).Load();
                db.Entry(v).Reference(p => p.Vendor).Load();
            }

            return PartialView("_GetProdByCatIdPartialView", VendorProductList);

        }

        public ActionResult GetProductsByCategoryId(int Cat_ID)
        {
            Category Cat = db.Categories.Where(i => i.ID == Cat_ID).FirstOrDefault();
            db.Entry(Cat).Collection(p => p.Products).Load();
            List<VendorProduct> VendorProductList = new List<VendorProduct>();
            foreach (var product in Cat.Products)
            {
               
                
                    db.Entry(product).Collection(p => p.VendorProducts).Load();
                    foreach (var VendorProduct in product.VendorProducts)
                    {
                    if (VendorProduct.Sale_price!=0&&VendorProduct.Sale_price !=null)
                    {
                        db.Entry(VendorProduct).Reference(p => p.Vendor).Load();
                        db.Entry(VendorProduct).Reference(p => p.Product).Load();
                        VendorProductList.Add(VendorProduct);
                    }
                    }
                  
                
            }
            return PartialView("_GetProdByCatIdPartialView", VendorProductList);
        }

    }
}