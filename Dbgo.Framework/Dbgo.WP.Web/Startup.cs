using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dbgo.WP.Web.Startup))]
namespace Dbgo.WP.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
