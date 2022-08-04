using Barbado_vl_Site.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Barbado_vl_Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataManager dataManager;

        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        
        public IActionResult Index()
        {
            return View(dataManager.TextFields.GetTextByCodeWord("PageIndex"));
        }

        public IActionResult Contacts()
        {
            return View(dataManager.TextFields.GetTextByCodeWord("PageContacts"));
        }
    }
}
