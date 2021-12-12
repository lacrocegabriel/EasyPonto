using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dev.EasyPonto.Startup))]
namespace Dev.EasyPonto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
