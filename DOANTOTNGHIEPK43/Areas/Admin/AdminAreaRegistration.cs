using System.Web.Mvc;

namespace DOANTOTNGHIEPK43.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                // xác định hôm này là hôm của admin
                                namespaces: new[] { "DOANTOTNGHIEPK43.Areas.Admin.Controllers" }


            );
        }
    }
}