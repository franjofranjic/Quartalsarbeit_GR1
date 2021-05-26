using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Quartalsarbeit_GR1.Startup))]
namespace Quartalsarbeit_GR1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
