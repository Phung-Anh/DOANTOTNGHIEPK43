using DOANTOTNGHIEPK43.Models;
using DOANTOTNGHIEPK43.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DOANTOTNGHIEPK43.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/News
        public ActionResult Index(string Searchtext, int ? page)
        {
            var pageSize = 5;
            if(page == null)
            {
                page = 1;
            }
            IEnumerable<News> items = db.News.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(x => x.Alias.Contains(Searchtext) || x.Title.Contains(Searchtext));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) :1 ;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News model)
        {
            if (ModelState.IsValid)
            {
                model.CreateDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.CategoryId = 1;
                model.Alias = DOANTOTNGHIEPK43.Models.Common.Filter.FilterChar(model.Title);
                db.News.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var item = db.News.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = DOANTOTNGHIEPK43.Models.Common.Filter.FilterChar(model.Title);
                db.News.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Deleted(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                db.News.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isAcive = item.IsActive });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids)) // kiểm tra có null hay không
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        if (int.TryParse(item, out int id)) //kiểm tra chuỗi nếu đúng sau đâu nó sẽ gán cho id cho từng biến id
                        {
                            var obj = db.News.Find(id);
                            if (obj != null) // kiểm tra id có tồn tại hay không nếu có nó sẽ remove
                            {
                                db.News.Remove(obj);
                            }
                        }
                    }
                    db.SaveChanges(); // lưu lại thông tin
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }

    }
}