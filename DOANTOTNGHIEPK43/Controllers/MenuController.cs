using DOANTOTNGHIEPK43.Models;
using DOANTOTNGHIEPK43.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANTOTNGHIEPK43.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var items = db.Cantegories.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop", items);
        }
        public ActionResult MenuProductCategory()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", items);
        }
        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.CateId = id;
            }
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuLeft", items);
        }

        public ActionResult Menuarrivals()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_Menuarrivals", items);
        }
    }

}