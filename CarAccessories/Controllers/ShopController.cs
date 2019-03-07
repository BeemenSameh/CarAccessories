using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using CarAccessories.Models.ViewModel;
//using CarAccessories.Models.ViewModels;

namespace CarAccessories.Controllers
{
    public class ShopController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Shop
        public ActionResult Index()
        {
            BrandsProducts b = new BrandsProducts
            {
                Brands = db.Brands.ToList(),
                VendorProducts = db.VendorProducts.ToList(),
                CategoriesList = db.Categories.ToList()
            };
            return View(b);
        }

        public ActionResult GetProductsByModelId(int Model_ID)
        {
            //{  ? p.Price == f : p.Sale_price == f}
            List<Product> ProductsList = db.Products.Where(i => i.Model.ID == Model_ID).ToList();


            return PartialView("_GetProdByCatIdPartialView", ProductsList);

        }

        public ActionResult GetAllProducts()
        {
            List<Product> ProductList = db.Products.ToList();
            return PartialView("_AllProductsPartialView", ProductList);
        }

        //public ActionResult Sale()
        //{
        //    CategoryProducts Shop = new CategoryProducts
        //    {
        //        Products = db.Products.Where(i => i.Sale_price != 0).ToList(),
        //        Categories = db.Categories.ToList(),
        //    };
        //    return View("Index", Shop);
        //}

        //public ActionResult addCartDetailsToDataBase(string ProductName)
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;
        //    if (claimsIdentity != null)
        //    {

        //        var userIdClaim = claimsIdentity.Claims
        //            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

        //        if (userIdClaim != null)
        //        {
        //            var userIdValue = userIdClaim.Value;
        //            Customer c = db.Customers.Where(i => i.ID == userIdValue).FirstOrDefault();

        //            db.Entry(c).Collection(o => o.Orders).Load();
        //            foreach (var o in c.Orders)
        //            {
        //                if (o.Isbuy == false)
        //                {
        //                    OrderDetails od = new OrderDetails();
        //                    od.Price = db.Products.Where(p => p.Name == ProductName).Select(p => p.Price).FirstOrDefault();
        //                    od.VendorProduct.Product = db.Products.Where(p => p.Name == ProductName).FirstOrDefault();

        //                    break;
        //                    // o.OrderDetails.Add();
        //                }
        //            }

        //            //c.IsPaid = false;
        //            db.Carts.Add(c);
        //            db.SaveChanges();
        //            return View();
        //        }
        //        else
        //        {
        //            return Redirect("/Account/Login");

        //        }

        //    }
        //    else
        //    {
        //        return Redirect("/Account/Register");
        //    }

        //}

        //public ActionResult ShowProductDetails(int id)
        //{

        //    Product p = db.Products.Where(i => i.ID == id).FirstOrDefault();

        //    return View("_ProductDetailsView", p);
        //}

        public ActionResult GetProductsByCategoryId(int Cat_ID)
        {
            List<Product> ProductsList = db.Categories.Where(i => i.ID == Cat_ID).Select(p => p.Products).FirstOrDefault().ToList();
            return PartialView("_GetProdByCatIdPartialView", ProductsList);
        }

    }
}