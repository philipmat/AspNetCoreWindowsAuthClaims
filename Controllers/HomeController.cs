using System.Diagnostics;
using AspNetCoreWindowsAuthClaims.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
