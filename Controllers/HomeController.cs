using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreWindowsAuthClaims.Models;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreWindowsAuthClaims.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Policy = Auth.Constants.HasMagicPowersPolicy)]
        public IActionResult Yes()
        {
            return Content($"{User.Identity.Name}: Has cheezeburger!");
        }

        // change MagicPowersInfoProvider to return false 
        [Authorize(Policy = Auth.Constants.NeverHasMagicPowersPolicy)]
        public IActionResult No()
        {
            return Content($"{User.Identity.Name}: NO.");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
