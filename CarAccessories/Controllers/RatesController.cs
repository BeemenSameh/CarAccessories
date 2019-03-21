using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarAccessories.Models;
using Microsoft.AspNet.Identity;

namespace CarAccessories.Controllers
{
    public class RatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        [Authorize]
        public ActionResult UserRateVendorProduct(int rate,int VendorProdId)
        {
            Rate r = new Rate();
            r.Customer_ID = User.Identity.GetUserId();
          Rate existRate = db.Rates.Where(u => u.Customer_ID == r.Customer_ID).Where(v => v.VendorProduct_ID == VendorProdId).FirstOrDefault();
            if(existRate==null)
            {
                r.RateNumber = rate;
                r.VendorProduct_ID = VendorProdId;

                db.Rates.Add(r);
                db.SaveChanges();
                //  return View();

                return Json(true,JsonRequestBehavior.AllowGet);
            }
            else
            {
                existRate.RateNumber = rate;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);

            }






        }

        public string RateVenProd(int VendorProdId)
        {
            int RateSum = 0;
            List<Rate> Rates = db.Rates.Where(i => i.VendorProduct_ID == VendorProdId).ToList();
            foreach(var item in Rates)
            {
                RateSum += item.RateNumber;
            }
            // int RateAverage = RateSum / Rates.Count;
            if (Rates.Count != 0)
            {
                //return Json(new { RateAvg = (RateSum / Rates.Count).ToString(), DivId = VendorProdId });
                return (RateSum / Rates.Count).ToString();
            }
            else return 0.ToString();
                //return  Json(new { RateAvg =0.ToString(), DivId = VendorProdId });
           // return Json(true, JsonRequestBehavior.AllowGet);
        }
        // GET: Rates
        public ActionResult Index()
        {
            var rates = db.Rates.Include(r => r.Customer).Include(r => r.VendorProduct);
            return View(rates.ToList());
        }

        // GET: Rates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }

        // GET: Rates/Create
        public ActionResult Create()
        {
            ViewBag.Customer_ID = new SelectList(db.Customers, "ID", "Name");
            ViewBag.VendorProduct_ID = new SelectList(db.VendorProducts, "ID", "ID");
            return View();
        }

        // POST: Rates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,VendorProduct_ID,Customer_ID,RateNumber")] Rate rate)
        {
            if (ModelState.IsValid)
            {
                db.Rates.Add(rate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customer_ID = new SelectList(db.Customers, "ID", "Name", rate.Customer_ID);
            ViewBag.VendorProduct_ID = new SelectList(db.VendorProducts, "ID", "ID", rate.VendorProduct_ID);
            return View(rate);
        }

        // GET: Rates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_ID = new SelectList(db.Customers, "ID", "Name", rate.Customer_ID);
            ViewBag.VendorProduct_ID = new SelectList(db.VendorProducts, "ID", "ID", rate.VendorProduct_ID);
            return View(rate);
        }

        // POST: Rates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,VendorProduct_ID,Customer_ID,RateNumber")] Rate rate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customer_ID = new SelectList(db.Customers, "ID", "Name", rate.Customer_ID);
            ViewBag.VendorProduct_ID = new SelectList(db.VendorProducts, "ID", "ID", rate.VendorProduct_ID);
            return View(rate);
        }

        // GET: Rates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }

        // POST: Rates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rate rate = db.Rates.Find(id);
            db.Rates.Remove(rate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
