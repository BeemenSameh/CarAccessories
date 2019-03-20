using CarAccessories.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    public class BaughtProductsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // List<Product> ProductList = new List<Product>();
        // GET: BaughtProducts
        public ActionResult Index()
        {
           
            List<OrderDetails> orders = new List<OrderDetails>();
                    var userIdValue = User.Identity.GetUserId();

                    Customer user = db.Customers.Where(i => i.ID == userIdValue).FirstOrDefault();
                    db.Entry(user).Collection(c => c.Orders).Load();
                    foreach (var i in user.Orders)
                    {
                        db.Entry(i).Collection(c => c.OrderDetails).Load();
                        foreach (var OrderDetails in i.OrderDetails)
                        {
                            db.Entry(i).Collection(c => c.OrderDetails).Load();
                            foreach (var Orderdetails in i.OrderDetails)
                            {
                                db.Entry(Orderdetails).Reference(c => c.VendorProduct).Load();
                                db.Entry(Orderdetails.VendorProduct).Reference(c => c.Product).Load();
                            }


                        }

                    }
                    orders.AddRange(db.OrderDetails.Where(c => c.Order.Isbuy == true));
                    return View(orders);
                }
              
            }
        }
    
