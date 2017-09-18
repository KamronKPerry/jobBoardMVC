using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FPJobBoard.UI.Startup))]
namespace FPJobBoard.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
