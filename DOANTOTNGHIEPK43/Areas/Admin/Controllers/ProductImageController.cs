﻿using DOANTOTNGHIEPK43.Models;
using DOANTOTNGHIEPK43.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANTOTNGHIEPK43.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]

    public class ProductImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProductImage
        public ActionResult Index( int id)
        {
            ViewBag.ProductId = id;
            var items = db.ProductImages.Where(x => x.ProductId == id).ToList();
             return View(items);
        }
        [HttpPost]
        public ActionResult AddImage(int productId, string url)
        {
            db.ProductImages.Add(new ProductImage
            {
                ProductId = productId,
                Image = url,
                IsDefault = false
            });
            db.SaveChanges();
            return Json(new { Success = true });
        }

        //[HttpPost]
        //public ActionResult Deleted (int id)
        //{
        //    var item = db.ProductImages.Find(id);
        //    db.ProductImages.Remove(item);
        //    db.SaveChanges();  
        //    return Json(new {success= true});

        //}
        [HttpPost]
        public ActionResult Deleted(int id)
        {
            var item = db.ProductImages.Find(id);
            if (item != null)
            {
                db.ProductImages.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

    }
}