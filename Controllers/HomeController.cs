using Microsoft.AspNetCore.Mvc;

namespace ClaimsTrackingSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
