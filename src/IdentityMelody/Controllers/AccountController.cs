using System.Web.Mvc;
using System.Web.Security;
using IdentityMelody.Infrastructure;
using IdentityMelody.Models;
using Microsoft.AspNet.Identity;

namespace IdentityMelody.Controllers
{
    public class AccountController : Controller
    {
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View();

            using (var userManager = IdentityMelodyUserManager.Create())
            {
                var user = userManager.Find(model.UserName, model.Password);

                if (user != null)
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.SetAuthCookie(model.UserName, false);

                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }

                ModelState.AddModelError("", "Incorrect username or password");
                return View();
            }
        }
    }
}