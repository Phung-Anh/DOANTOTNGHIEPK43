using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DOANTOTNGHIEPK43.Startup))]
namespace DOANTOTNGHIEPK43
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
