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
            foreach (var vendorProduct in b.VendorProducts)
            {
                db.Entry(vendorProduct).Reference(p => p.Product).Load();
                db.Entry(vendorProduct).Reference(v => v.Vendor).Load();
            }
            return View(b);
        }
        
        public ActionResult GetAllProducts()
        {
            List<VendorProduct> AllVendorProductsList = db.VendorProducts.ToList();
            foreach (var i in AllVendorProductsList)
            {
                db.Entry(i).Reference(p => p.Product).Load();
                db.Entry(i).Reference(p => p.Vendor).Load();
            }
            return PartialView("_AllProductsPartialView", AllVendorProductsList);
        }

        public ActionResult addCartDetailsToDataBase(string ProductName)
        {
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    Customer c = db.Customers.Where(i => i.ID == userIdValue).FirstOrDefault();
                    db.Entry(c).Collection(o => o.Orders).Load();
                    foreach (var o in c.Orders)
                    {
                        if (o.Isbuy == false)
                        {
                            Product product = db.Products.Where(p => p.Name == ProductName).FirstOrDefault();
                            OrderDetails od = new OrderDetails
                            {
                                Price = db.VendorProducts.Where(vp => vp.Product.Equals(product)).Select(vp => vp.Price).FirstOrDefault(),
                                Quantity = 0
                            };
                            od.VendorProduct.Product = db.Products.Where(p => p.Name == ProductName).FirstOrDefault();
                            o.OrderDetails.Add(od);
                            db.SaveChanges();
                            break;
                        }
                        else
                        {
                            Order order = new Order();
                            order.Isbuy = false;
                            order.OrderDetails = new List<OrderDetails>();
                            Product product = db.Products.Where(p => p.Name == ProductName).FirstOrDefault();
                            OrderDetails od = new OrderDetails
                            {
                                Price = db.VendorProducts.Where(vp => vp.Product.ID == product.ID).Select(vp => vp.Price).FirstOrDefault(),
                                Quantity = 0
                            };
                            db.Entry(od).Reference(vp => vp.VendorProduct).Load();
                            od.VendorProduct.Product = db.Products.Where(p => p.Name == ProductName).FirstOrDefault();
                            o.OrderDetails.Add(od);
                            db.SaveChanges();
                            break;
                        }
                    }
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
        public ActionResult filterProductsByPrice(int LowerPrice, int UpperPrice)
        {
            List<VendorProduct> price = db.VendorProducts.Where(v => v.Price == LowerPrice && v.Price == UpperPrice).ToList();
            foreach (var i in price)
            {
                db.Entry(i).Reference(p => p.Product).Load();
                db.Entry(i).Reference(p => p.Vendor).Load();
            }
            return PartialView("_AllProductsPartialView", price);
        }
    }
}