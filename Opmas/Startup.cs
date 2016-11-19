using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Opmas.Startup))]
namespace Opmas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
