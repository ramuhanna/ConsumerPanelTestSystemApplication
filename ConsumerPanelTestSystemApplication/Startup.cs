using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConsumerPanelTestSystemApplication.Startup))]
namespace ConsumerPanelTestSystemApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
