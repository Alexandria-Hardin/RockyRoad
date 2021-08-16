using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RockyRoad.WebMVC.Startup))]
namespace RockyRoad.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
