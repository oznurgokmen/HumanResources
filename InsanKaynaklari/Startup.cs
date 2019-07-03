using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InsanKaynaklari.Startup))]
namespace InsanKaynaklari
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
