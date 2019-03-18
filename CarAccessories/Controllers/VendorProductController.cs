﻿using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    public class VendorProductController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: VendorProduct
        public ActionResult GetProducts()
        {
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

                    //db.Entry(user).Reference(vendor => vendor.Vendor).Load();
                    db.Entry(user).Collection(prod => prod.VendorProduct).Load();
                    foreach (var VP in user.VendorProduct)
                    {
                        db.Entry(VP).Reference(prod => prod.Product).Load();
                        db.Entry(VP.Product).Reference(cat => cat.Category).Load();
                    }
                }
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

        // GET: vendor/Edit/5
        public ActionResult EditProduct(int id)
        {
            var cats = db.Categories.ToList();
            ViewBag.category = cats;
            //ViewBag.ID = uid;
            var Models = db.Models.ToList();
            ViewBag.Model = Models;
            Product product = db.Products.FirstOrDefault(prod => prod.ID == id);
            return PartialView("_EditProduct",product);
        }

        // POST: vendor/Edit/5
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

        // GET: vendor/Delete/5
        public ActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if(product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("GetPartialProducts");
            }

            return HttpNotFound();
        }

        
        public ActionResult GetPartialProducts()
        {
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

                    //db.Entry(user).Reference(vendor => vendor.Vendor).Load();
                    db.Entry(user).Collection(prod => prod.VendorProduct).Load();
                    foreach (var VP in user.VendorProduct)
                    {
                        db.Entry(VP).Reference(prod => prod.Product).Load();
                        db.Entry(VP.Product).Reference(cat => cat.Category).Load();
                    }
                }
            }

            return PartialView("_GetPartialProducts", user);
        }
    }
}