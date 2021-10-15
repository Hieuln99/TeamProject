using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(CustomLog.Startup))]
namespace CustomLog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var authOptions = new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                ExpireTimeSpan = TimeSpan.FromMinutes(7),
            };
            app.UseCookieAuthentication(authOptions);
        }
    }
}
