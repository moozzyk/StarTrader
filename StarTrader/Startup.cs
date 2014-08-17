using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StarTrader.Startup))]
namespace StarTrader
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
