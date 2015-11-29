using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PP.Web.Startup))]
namespace PP.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
