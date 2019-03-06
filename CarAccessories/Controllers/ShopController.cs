using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using CarAccessories.Models;
//using CarAccessories.Models.ViewModels;

namespace CarAccessories.Controllers
{
    public class ShopController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Shop
        public ActionResult Index()
        {
            List<Product> ProductsList = db.Products.ToList();

            return View(ProductsList);
        }

        //public ActionResult GetProductsByCatId(int Category_ID)
        //{
        //    //{  ? p.Price == f : p.Sale_price == f}
        //    List<Product> ProductsList = db.Products.Where(i => i.Category.ID == Category_ID).ToList();


        //    return PartialView("_GetProdByCatIdPartialView", ProductsList);

        //}

    

        public ActionResult GetAllProducts()
        {
            List<Product> ProductList = db.Products.ToList();
            return PartialView("_AllProductsPartialView", ProductList);
        }

        //public ActionResult Sale()
        //{
        //    CategoryProducts Shop = new CategoryProducts
        //    {
        //        Products = db.Products.Where(i=>i.Sale_price!=0).ToList(),
        //        Categories = db.Categories.ToList(),
        //    };
        //    return View("Index", Shop);
        //}

        public ActionResult addCartDetailsToDataBase(string ProductName)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {

                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    Order c = new Order();
                    c.Customer = db.Customers.Where(i => i.ID == userIdValue).FirstOrDefault();

                    //c.OrderDetails = db.Products.Where(p => p.Name == ProductName).FirstOrDefault();
                    //c. = 1;
                    //c.IsPaid = false;
                    db.Carts.Add(c);
                    db.SaveChanges();
                    return View();
                }
                else
                {
                    return Redirect("/Account/Login");

                }

            }
            else
            {
                return Redirect("/Account/Register");
            }

        }

        public ActionResult ShowProductDetails(int id)
        {

            Product p = db.Products.Where(i => i.ID == id).FirstOrDefault();
          
            return View("_ProductDetailsView", p);
        }



    }
}