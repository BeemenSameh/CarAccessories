using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    public class AdminOrderController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminOrder
        public ActionResult Index()
        {
            var Orders = db.Orders.ToList();
            return View(Orders);
        }
        [ActionName("GetOrder")]
        public ActionResult GetOrderByID(int id)
        {
            var order = db.Orders.FirstOrDefault(ord => ord.ID == id);
            db.Entry(order).Reference(c => c.Customer).Load();
            db.Entry(order).Collection(od => od.OrderDetails).Load();
            db.Entry(order.Customer).Reference(au => au.ApplicationUser).Load();
            foreach (var orderdetails in order.OrderDetails)
            {
                db.Entry(orderdetails).Reference(vend => vend.VendorProduct).Load();
                db.Entry(orderdetails.VendorProduct).Reference(prod => prod.Product).Load();
                db.Entry(orderdetails.VendorProduct).Reference(vend => vend.Vendor).Load();
                db.Entry(orderdetails.VendorProduct.Vendor).Reference(au => au.ApplicationUser).Load();

            }
            return View("GetOrderByID", order);
        }
    }
}