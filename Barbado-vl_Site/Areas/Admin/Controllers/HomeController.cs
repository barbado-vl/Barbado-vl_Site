﻿using Microsoft.AspNetCore.Mvc;

namespace Barbado_vl_Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
