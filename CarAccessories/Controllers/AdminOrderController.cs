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
            return View();
        }
        public ActionResult GetOrderByID(int id)
        {
            var order = db.Orders.FirstOrDefault(ord => ord.ID == id);
            return View(order);
        }
    }
}