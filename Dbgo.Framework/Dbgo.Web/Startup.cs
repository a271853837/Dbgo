using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dbgo.Web.Startup))]
namespace Dbgo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
