using Microsoft.AspNetCore.Mvc;

namespace Barbado_vl_Site.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
