using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarAccessories.Startup))]
namespace CarAccessories
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
