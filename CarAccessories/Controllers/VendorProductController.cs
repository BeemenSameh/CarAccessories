using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    public class VendorProductController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: VendorProduct
        public ActionResult GetProducts(string id)
        {
            var user = db.Users.FirstOrDefault(use => use.Id == id);
            db.Entry(user).Reference(vendor => vendor.Vendor).Load();
            db.Entry(user.Vendor).Collection(prod => prod.VendorProduct).Load();
            foreach (var VP in user.Vendor.VendorProduct)
            {
                db.Entry(VP).Reference(prod => prod.Product).Load();
                db.Entry(VP.Product).Reference(cat => cat.Category).Load();
            }
            return View(user);
        }
        public ActionResult addProduct(string id)
        {
            ViewBag.ID = id;
            var Models = db.Models.ToList();
            ViewBag.Model = Models;
            return PartialView("_addProduct",new Product());
        }

        
        [HttpPost]
        public ActionResult addProduct(Product product)
        {
            // TODO: Add insert logic here
            if (ModelState.IsValid == true)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("GetAllProducts");
            }

            else { return View(product); }

        }

        // GET: Vedor/Edit/5
        public ActionResult EditProduct(string uid,int? pid)
        {
            ViewBag.ID = uid;
            var Models = db.Models.ToList();
            ViewBag.Model = Models;
            Product product = db.Products.FirstOrDefault(prod => prod.ID == pid);
            return PartialView("_EditProduct",product);
        }

        // POST: Vedor/Edit/5
        [HttpPost]
        public ActionResult EditProduct(int id, Product product)
        {
            // TODO: Add update logic here
            if (ModelState.IsValid == true)
            {
                Product ProductFromDb = db.Products.Find(id);
                ProductFromDb.Name = product.Name;
                ProductFromDb.Image = product.Image;
                ProductFromDb.MinDescription = product.MinDescription;
                //ProductFromDb.Category. = product.State;
                return RedirectToAction("GetAllProducts");
            }
            else
            {
                return View(product);
            }
        }

        // GET: Vedor/Delete/5
        public ActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);

            return PartialView("_DeleteProduct", product);
        }

        // POST: Vedor/Delete/5
        [HttpPost]
        public ActionResult DeleteProduct(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult GetPartialProducts(string id)
        {
            var user = db.Users.FirstOrDefault(use => use.Id == id);
            db.Entry(user).Reference(vendor => vendor.Vendor).Load();
            db.Entry(user.Vendor).Collection(prod => prod.VendorProduct).Load();
            foreach (var VP in user.Vendor.VendorProduct)
            {
                db.Entry(VP).Reference(prod => prod.Product).Load();
                db.Entry(VP.Product).Reference(cat => cat.Category).Load();
            }
            return PartialView("_GetPartialProducts", user);
        }
    }
}