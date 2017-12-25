using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Lottery.WebApi.Startup))]

namespace Lottery.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
