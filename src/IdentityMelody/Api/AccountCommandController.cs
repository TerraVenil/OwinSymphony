using System.Linq;
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
    [RoutePrefix("api/v1/account")]
    public class AccountCommandController : ApiController
    {
        private readonly IdentityMelodyUserManager _userManager;

        public AccountCommandController(IdentityMelodyUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IHttpActionResult Login(LoginViewModel model, [FromUri]string returnUrl)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = _userManager.Find(model.UserName, model.Password);

            if (user != null)
            {
                var authentictionManager = HttpContext.Current.GetOwinContext().Authentication;
                authentictionManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                var userIdentity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authentictionManager.SignIn(new AuthenticationProperties { IsPersistent = false }, userIdentity);

                return Ok();
            }

            return new UnauthorizedResult(new AuthenticationHeaderValue[] { }, this);
        }

        [HttpGet]
        [Authorize]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(_userManager.Users.Select(x => new { x.Id, x.Email, x.UserName }));
        }
    }
}