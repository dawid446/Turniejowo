using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(programowanie_wsi.Startup))]
namespace programowanie_wsi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
