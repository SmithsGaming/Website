using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmithsModding_Website.Startup))]
namespace SmithsModding_Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
