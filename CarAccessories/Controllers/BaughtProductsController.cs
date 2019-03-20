﻿using CarAccessories.Models;
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
        List<Product> ProductList = new List<Product>();
       // GET: BaughtProducts
        public ActionResult Index()
        { 

                  var userIdValue = User.Identity.GetUserId();
                  Customer user = db.Customers.Where(i => i.ID == userIdValue).FirstOrDefault();
                    
                        db.Entry(user).Collection(c => c.Orders).Load();
                        foreach (var i in user.Orders)
                        {
                            db.Entry(i).Collection(c => c.OrderDetails).Load();
                            foreach (var OrderDetails in i.OrderDetails)
                            {
                                db.Entry(OrderDetails).Reference(c => c.VendorProduct).Load();
                                db.Entry(OrderDetails.VendorProduct).Reference(c => c.Product).Load();
                            }


                        }

                    
                    return View(user);
               //}
               // else
               // {
               //     return Redirect("/Account/Login");

               // }


            //}
            //else
            //{
            //    return Redirect("/Account/Register");
            //}

        }
          
    }
}