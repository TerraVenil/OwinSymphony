using System.Web.Mvc;
using IdentityMelody.Infrastructure;
using IdentityMelody.Models;
using Microsoft.AspNet.Identity;

namespace IdentityMelody.Controllers
{
    public class HomeController : Controller
    {
        private readonly IdentityMelodyUserManager _manager;

        public HomeController(IdentityMelodyUserManager manager)
        {
            _manager = manager;
        }

        public ActionResult Index()
        {
            using (var userManager = _manager)
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
            using (var userManager = _manager)
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

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}