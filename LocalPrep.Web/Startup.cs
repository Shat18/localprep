using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocalPrep.Web.Startup))]
namespace LocalPrep.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
