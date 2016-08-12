using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonalSystem.Startup))]
namespace PersonalSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
