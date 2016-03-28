using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using IdentityMelody.Infrastructure;
using IdentityMelody.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace IdentityMelody.Api
{
    [AllowAnonymous]
    [RoutePrefix("api/v1/account")]
    public class AccountCommandController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginViewModel model, [FromUri]string returnUrl)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            using (var userManager = IdentityMelodyUserManager.Create())
            {
                var user = userManager.Find(model.UserName, model.Password);

                if (user != null)
                {
                    var authentictionManager = HttpContext.Current.GetOwinContext().Authentication;
                    authentictionManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authentictionManager.SignIn(new AuthenticationProperties { IsPersistent = false }, userIdentity);

                    return Ok();
                }
            }

            return new UnauthorizedResult(new AuthenticationHeaderValue[] { }, this);
        }
    }
}