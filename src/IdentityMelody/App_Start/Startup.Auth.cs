using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SimpleInjector.Integration.WebApi;

namespace IdentityMelody
{
    public partial class Startup
    {
         // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

            var container = DependencyConfig.RegisterDependencies();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            app.UseWebApi(config);
        }
    }
}