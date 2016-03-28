using System.Web.Mvc;
using IdentityMelody.Infrastructure;
using IdentityMelody.Models;
using Microsoft.AspNet.Identity;

namespace IdentityMelody.Controllers
{
    public class HomeController : Controller
    {
        private readonly IdentityMelodyUserManager _userManager;

        public HomeController(IdentityMelodyUserManager userManager)
        {
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            return View(_userManager.Users);
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
            var result = _userManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            AddErrorsFromResult(result);

            return View(model);
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