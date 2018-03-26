using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(C_Market.Startup))]
namespace C_Market
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
