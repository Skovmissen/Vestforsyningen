using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vestforsyningen.Startup))]
namespace Vestforsyningen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
