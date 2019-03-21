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
                db.Entry(i).Collection(p => p.Rates).Load();
            }
            return PartialView("_AllProductsPartialView", AllVendorProductsList);
        }

        public ActionResult addCartDetailsToDataBase(string ProductName, string VendorName)
        {
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    Customer c = db.Customers.FirstOrDefault(i => i.ID == userIdValue);
                    db.Entry(c).Collection(o => o.Orders).Load();
                    if (c.Orders.Count > 0)
                    {
                        foreach (var o in c.Orders)
                        {
                            if (o.Isbuy == false)
                            {
                                Product product = db.Products.Where(p => p.Name == ProductName).FirstOrDefault();
                                Vendor Vendor = db.Vendors.Where(v => v.ComponyName == VendorName).FirstOrDefault();
                                VendorProduct vendorProduct = db.VendorProducts.FirstOrDefault(vp => vp.Product.ID == product.ID && vp.Vendor.ID == Vendor.ID);
                                OrderDetails od = new OrderDetails
                                {
                                    VendorProduct = vendorProduct,
                                    Price = vendorProduct.Price,
                                    Quantity = 0,
                                };
                                o.OrderDetails.Add(od);
                                db.SaveChanges();
                                break;
                            }
                            else
                            {
                                Order order = new Order
                                {
                                    Isbuy = false,
                                    TotalPrice = 0,
                                    Customer = c,
                                    OrderDetails = new List<OrderDetails>()
                                };
                                Product product = db.Products.Where(p => p.Name == ProductName).FirstOrDefault();
                                Vendor Vendor = db.Vendors.Where(v => v.ComponyName == VendorName).FirstOrDefault();
                                VendorProduct vendorProduct = db.VendorProducts.FirstOrDefault(vp => vp.Product.ID == product.ID && vp.Vendor.ID == Vendor.ID);
                                OrderDetails od = new OrderDetails
                                {
                                    VendorProduct = vendorProduct,
                                    Price = vendorProduct.Price,
                                    Quantity = 0,
                                };
                                order.OrderDetails.Add(od);
                                db.Orders.Add(order);
                                db.SaveChanges();
                                break;
                            }
                        }
                    }
                    else
                    {
                        Order order = new Order
                        {
                            Isbuy = false,
                            TotalPrice = 0,
                            Customer = c,
                            OrderDetails = new List<OrderDetails>()
                        };
                        Product product = db.Products.Where(p => p.Name == ProductName).FirstOrDefault();
                        Vendor Vendor = db.Vendors.Where(v => v.ComponyName == VendorName).FirstOrDefault();
                        VendorProduct vendorProduct = db.VendorProducts.FirstOrDefault(vp => vp.Product.ID == product.ID && vp.Vendor.ID == Vendor.ID);
                        OrderDetails od = new OrderDetails
                        {
                            VendorProduct = vendorProduct,
                            Price = vendorProduct.Price,
                            Quantity = 0,
                        };
                        order.OrderDetails.Add(od);
                        db.Orders.Add(order);
                        db.SaveChanges();
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

        public ActionResult GetProductsByCategoryId(int Cat_ID)
        {
            Category Cat = db.Categories.Where(i => i.ID == Cat_ID).FirstOrDefault();
            db.Entry(Cat).Collection(p => p.Products).Load();
            List<VendorProduct> VendorProductList = new List<VendorProduct>();
            foreach (var pl in Cat.Products)
            {
                db.Entry(pl).Collection(p => p.VendorProducts).Load();
                foreach (var VendorProducts in pl.VendorProducts)
                {
                    db.Entry(VendorProducts).Reference(p => p.Vendor).Load();
                    db.Entry(VendorProducts).Reference(p => p.Product).Load();
                }
                VendorProductList.AddRange(pl.VendorProducts);
            }
            return PartialView("_GetProdByCatIdPartialView", VendorProductList);
        }

        public ActionResult filterProductsByPrice(int LowerPrice, int UpperPrice)
        {
           
            List<VendorProduct> price = db.VendorProducts.Where(v => v.Price >= LowerPrice && v.Price <= UpperPrice).ToList();
            foreach (var i in price)
            {
                db.Entry(i).Reference(p => p.Product).Load();
                db.Entry(i).Reference(p => p.Vendor).Load();
            }

            return PartialView("_AllProductsPartialView", price);
        }
    }
}