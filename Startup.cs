using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project2016.Startup))]
namespace Project2016
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
