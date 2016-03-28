using System.Reflection;
using System.Web.Mvc;
using IdentityMelody.Infrastructure;
using IdentityMelody.Models;
using Microsoft.AspNet.Identity;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace IdentityMelody
{
    public static class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            // https://simpleinjector.readthedocs.org/en/latest/mvcintegration.html
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<IdentityMelodyDbContext>(Lifestyle.Scoped);
            container.Register<IUserStore<MusicUser>, IdentityMelodyUserStore>(Lifestyle.Scoped);
            container.Register<IdentityMelodyUserManager>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}