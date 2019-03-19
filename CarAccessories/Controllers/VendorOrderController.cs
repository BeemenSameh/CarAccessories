using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    public class VendorOrderController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: VendorOrder
        public ActionResult Index()
        {

            ICollection<OrderDetails> ordetail = new List<OrderDetails>();
            var user = new Vendor();
            bool claimIdentity = User.Identity is ClaimsIdentity;
            if (claimIdentity)
            {
                ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    user = db.Vendors.Where(i => i.ID == userIdValue).FirstOrDefault();

                    var orderDetails = (from vend in db.VendorProducts
                                        where vend.Vendor.ID == user.ID
                                        select vend.OrderDetails).FirstOrDefault();
                    ordetail = orderDetails;
                    //db.Entry()
                    //foreach (var OD in orderDetails)
                    //{
                    //    db.Entry(OD).Collection(o=>o).ToLoad();
                    //    //ordetail = OD;
                    //    //db.Entry(order).Collection()
                    //    foreach (var item in OD)
                    //    {
                    //        //db.Entry(item)
                    //    }
                    //}


                            //db.Entry(user).Reference(vendor => vendor.Vendor).Load();
                            db.Entry(user).Collection(prod => prod.VendorProduct).Load();
                    foreach (var VP in user.VendorProduct)
                    {
                        db.Entry(VP).Reference(prod => prod.Product).Load();
                        db.Entry(VP.Product).Reference(cat => cat.Category).Load();
                    }
                }
            }
          
            //var Orders = db.Orders.ToList();
            //foreach (var order in Orders)
            //{
            //    foreach (var vp in order.OrderDetails)
            //    {
            //        db.Entry(vp).Reference(i => i.VendorProduct).Load();
            //        db.Entry(vp.VendorProduct).Reference(p => p.Vendor).Load();
            //    }
            //    //db.Entry(order).Reference(cust => cust.Customer).Load();
            //}
            return PartialView("_VendorOrders",ordetail);
        }
    }
}