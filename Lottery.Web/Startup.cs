using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lottery.Web.Startup))]

namespace Lottery.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}