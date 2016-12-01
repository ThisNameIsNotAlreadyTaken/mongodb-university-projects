using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(M101DotNet.WebApp.Startup))]
namespace M101DotNet.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
