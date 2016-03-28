using System.Web;
using System.Web.Mvc;
using IdentityMelody.Infrastructure;
using IdentityMelody.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace IdentityMelody.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View();

            using (var userManager = IdentityMelodyUserManager.Create())
            {
                var user = userManager.Find(model.UserName, model.Password);

                if (user != null)
                {
                    var authentictionManager = HttpContext.GetOwinContext().Authentication;
                    authentictionManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authentictionManager.SignIn(new AuthenticationProperties { IsPersistent = false }, userIdentity);

                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }

                ModelState.AddModelError("", "Incorrect username or password");
                return View();
            }
        }
    }
}