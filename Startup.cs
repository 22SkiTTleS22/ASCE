using ASCE.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASCE.Startup))]
namespace ASCE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
