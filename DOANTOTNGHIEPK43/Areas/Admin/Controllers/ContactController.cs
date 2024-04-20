using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANTOTNGHIEPK43.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]

    public class ContactController : Controller
    {
        // GET: Admin/Contact
        public ActionResult Index()
        {
            return View();
        }
    }
}