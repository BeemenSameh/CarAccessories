﻿using CarAccessories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarAccessories.Controllers
{
    public class AdminUsersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminUsers
        public ActionResult Index()
        {
            
            var users = db.Users.ToList();
            foreach (var user in users)
            {
                db.Entry(user).Reference(c => c.Customer).Load();
                db.Entry(user).Reference(sell => sell.Seller).Load();
            }
            return View(users);
        }
        public ActionResult DeleteUser(string id)
        {
            var user = db.Users.FirstOrDefault(use => use.Id == id);
            return View(user);
        }
        public ActionResult ConfirmDeleteUser(string id)
        {
            var user = db.Users.FirstOrDefault(use => use.Id == id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}