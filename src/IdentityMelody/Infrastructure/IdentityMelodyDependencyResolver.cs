using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IdentityMelody.Infrastructure
{
    public class IdentityMelodyDependencyResolver : IDependencyResolver
    {
        private readonly DependencyResolver _dependencyResolver;

        public IdentityMelodyDependencyResolver()
        {
            _dependencyResolver = new DependencyResolver();
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IControllerFactory))
            {
                return new IdentityMelodyControllerFactory();
            }

            return _dependencyResolver.InnerCurrent.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _dependencyResolver.InnerCurrent.GetServices(serviceType);
        }
    }
}