using DOANTOTNGHIEPK43.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANTOTNGHIEPK43.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index()
        {
            var items = db.Products.ToList();
           
            return View(items);
        }

        public ActionResult Detail(string alias,int id)
        {
            var items = db.Products.Find(id);
            return View(items);
        }   
        public ActionResult ProductCategory(string alias, int? id)
        {
            var items = db.Products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList(); // lọc dữ liệu được lấy ra dựa vào id của danh mục sản phẩm 
            }
            var cate = db.ProductCategories.Find(id);
            if(cate != null)
            {
                ViewBag.CateName = cate.Title; 
            }
            ViewBag.CateId = id;
            return View(items);
        }
        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(x => x.IsHome).Take(10).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_ProductSale()
        {
            var items = db.Products.Where(x => x.IsSale).Take(10).ToList();
            return PartialView(items);
        }
    }
}