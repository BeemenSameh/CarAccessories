using CarAccessories.Models;
using CarAccessories.Models.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    [Authorize]
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
        public ActionResult addProduct()
        {
            //var Models = db.Models.ToList();
            //var Cats = db.Categories.ToList();

            ProductCategoryModelVM ProductCatModelVM = new ProductCategoryModelVM()
            {
                Models = db.Models.ToList(),
                Categories = db.Categories.ToList()
            };

            return PartialView("_addProduct", ProductCatModelVM);
        }

        
        [HttpPost]
        public ActionResult addProduct(ProductCategoryModelVM ProductCatModelVM)
        {
            // TODO: Add insert logic here
            if (ModelState.IsValid == true)
            {
                var cat = db.Categories.Where(c => c.ID == ProductCatModelVM.CategoryId).FirstOrDefault();
                var mod = db.Models.Where(m=>m.ID== ProductCatModelVM.ModelId).FirstOrDefault();
                var product = new Product()
                {
                    Name = ProductCatModelVM.Name,
                    Category = cat,
                    Model = mod,
                    Image = ProductCatModelVM.Image,
                    MinDescription = ProductCatModelVM.MinDescription,
                    State = ProductCatModelVM.State,
                

                };
                db.Products.Add(product);
                VendorProduct vp = new VendorProduct();
                vp.Price = ProductCatModelVM.Price;
                vp.Sale_price = ProductCatModelVM.Sale_price;
                vp.Quantity = ProductCatModelVM.Quantity;
                vp.Product = product;
                string  vid = User.Identity.GetUserId();
                Vendor v = db.Vendors.Where(i => i.ID == vid).FirstOrDefault();
                vp.Vendor = v;
                db.VendorProducts.Add(vp);
                db.SaveChanges();
                return RedirectToAction("GetProducts");
            }

            else { return View("_addProduct", ProductCatModelVM); }

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
             
                Product ProductFromDb = db.Products.Find(id);
                ProductFromDb.Name = product.Name;
                ProductFromDb.Image = product.Image;
                ProductFromDb.MinDescription = product.MinDescription;
                //ProductFromDb.Category. = product.State;
                db.SaveChanges();
                return RedirectToAction("GetProducts");
             

                //return View(product);
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