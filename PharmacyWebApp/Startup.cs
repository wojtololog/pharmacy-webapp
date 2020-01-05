using Microsoft.Owin;
using Owin;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

[assembly: OwinStartupAttribute(typeof(PharmacyWebApp.Startup))]
namespace PharmacyWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}