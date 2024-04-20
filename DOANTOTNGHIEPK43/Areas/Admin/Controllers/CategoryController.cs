using DOANTOTNGHIEPK43.Models;
using DOANTOTNGHIEPK43.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANTOTNGHIEPK43.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]

    public class CategoryController : Controller

    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var items = db.Cantegories;
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            if (ModelState.IsValid)
            {
                model.CreateDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = DOANTOTNGHIEPK43.Models.Common.Filter.FilterChar(model.Title);
                db.Cantegories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = db.Cantegories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                db.Cantegories.Attach(model);
                model.ModifiedDate = DateTime.Now;
                model.Alias = DOANTOTNGHIEPK43.Models.Common.Filter.ChuyenCoDauThanhKhongDau(model.Title); // với đoạn mã này thì sẽ là thay đổi đường dẫn 
                db.Entry(model).Property(x => x.Title).IsModified = true; // IsModified nó sẽ báo cho model là thuộc tính này sẽ được cập nhật
                db.Entry(model).Property(x => x.Description).IsModified = true;
                db.Entry(model).Property(x => x.Alias).IsModified = true;
                db.Entry(model).Property(x => x.SeoDescription).IsModified = true;
                db.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
                db.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                db.Entry(model).Property(x => x.Position).IsModified = true;
                db.Entry(model).Property(x => x.ModifiedDate).IsModified = true;
                db.Entry(model).Property(x => x.ModifiedBy).IsModified = true;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Deleted(int id)
        {
            var item = db.Cantegories.Find(id);
            if (item != null)
            {
                db.Cantegories.Remove(item);
                db.SaveChanges();
                return Json(new {success = true});
            }
            return Json(new { success = false });

        }

    }


}