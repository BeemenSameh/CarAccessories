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
            BrandsProducts b = new BrandsProducts {
                Brands = db.Brands.ToList(),
                Products = db.Products.ToList()
            };  
      
            return View(b);
        }
        public ActionResult ShowThisBrandModels(int BrandId)
        {
            BrandModelsAndBrandProducts b = new BrandModelsAndBrandProducts {
                ModelsList = db.Models.Where(m => m.Brand.ID == BrandId).ToList(),
                ProductsList = db.Products.Where(p => p.Model.Brand.ID == BrandId).ToList()

            };

            return View(b);
        }
        

    }
}