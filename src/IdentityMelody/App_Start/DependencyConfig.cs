using System.Web.Http;
using IdentityMelody.Infrastructure;
using IdentityMelody.Models;
using Microsoft.AspNet.Identity;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace IdentityMelody
{
    public static class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            // https://simpleinjector.readthedocs.org/en/latest/webapiintegration.html
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            container.Register<IdentityMelodyDbContext>(Lifestyle.Scoped);
            container.Register<IUserStore<MusicUser>, IdentityMelodyUserStore>(Lifestyle.Scoped);
            container.Register<IdentityMelodyUserManager>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}