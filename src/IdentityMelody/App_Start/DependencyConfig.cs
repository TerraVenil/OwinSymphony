using System.Collections.Generic;
using System.Web;
using IdentityMelody.Infrastructure;
using IdentityMelody.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.WebApi;

namespace IdentityMelody
{
    public static class DependencyConfig
    {
        public static Container RegisterDependencies(IAppBuilder app)
        {
            // https://simpleinjector.readthedocs.org/en/latest/webapiintegration.html
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            // http://stackoverflow.com/questions/27447757/register-iauthenticationmanager-with-simple-injector
            container.RegisterPerWebRequest<IAuthenticationManager>(() =>
                container.IsVerifying()
                    ? new OwinContext(new Dictionary<string, object>()).Authentication
                    : HttpContext.Current.GetOwinContext().Authentication);

            container.Register<IdentityMelodyDbContext>(Lifestyle.Scoped);
            container.Register<IUserStore<MusicUser>, IdentityMelodyUserStore>(Lifestyle.Scoped);
            container.Register<IdentityMelodyUserManager>(Lifestyle.Scoped);

            container.Verify();

            return container;
        }
    }
}