using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkshopDotNet.Web.Startup))]
namespace WorkshopDotNet.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
