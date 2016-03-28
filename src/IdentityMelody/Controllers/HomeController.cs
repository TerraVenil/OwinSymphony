using System.Web;
using System.Web.Mvc;
using IdentityMelody.Infrastructure;
using IdentityMelody.Models;
using Microsoft.AspNet.Identity;

namespace IdentityMelody.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var userManager = IdentityMelodyUserManager.Create())
            {
                return View(userManager.Users);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateMusicUserModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new MusicUser { UserName = model.Name, Email = model.Email };
            using (var userManager = IdentityMelodyUserManager.Create())
            {
                var result = userManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                AddErrorsFromResult(result);
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            var authentictionManager = HttpContext.GetOwinContext().Authentication;
            authentictionManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return Redirect(Url.Action("Login", "Account"));
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}