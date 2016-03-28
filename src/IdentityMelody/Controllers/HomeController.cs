using System.Web.Mvc;
using IdentityMelody.Infrastructure;

namespace IdentityMelody.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var userManager = IdentityMelodyUserManager.Create())
            {
                return View(userManager.Users);
            }
        }
    }
}