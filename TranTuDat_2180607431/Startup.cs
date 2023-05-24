using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TranTuDat_2180607431.Startup))]
namespace TranTuDat_2180607431
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
