using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExcptionWeb.Startup))]
namespace ExcptionWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
