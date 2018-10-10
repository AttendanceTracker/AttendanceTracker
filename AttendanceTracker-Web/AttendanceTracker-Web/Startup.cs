using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AttendanceTracker_Web.Startup))]
namespace AttendanceTracker_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
