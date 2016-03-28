using System.Collections.Generic;
using System.Web.Mvc;

namespace IdentityMelody.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var data = new Dictionary<string, object> { { "Placeholder", "Placeholder" } };
            return View(data);
        }
    }
}