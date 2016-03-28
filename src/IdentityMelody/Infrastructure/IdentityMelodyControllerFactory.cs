using System.Web.Mvc;
using System.Web.Routing;
using IdentityMelody.Controllers;
using IdentityMelody.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityMelody.Infrastructure
{
    public class IdentityMelodyControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (controllerName == "Home")
            {
                return new HomeController(new IdentityMelodyUserManager(new UserStore<MusicUser>(new IdentityMelodyDbContext())));
            }

            return base.CreateController(requestContext, controllerName);
        }
    }
}