using Microsoft.AspNetCore.Mvc;

namespace ClaimsTrackingSystem.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            ViewData["ErrorMessage"] = statusCode switch
            {
                404 => "Sorry, the resource you requested could not be found.",
                500 => "An internal server error has occurred. Please try again later.",
                _ => "An unexpected error has occurred. Please try again."
            };
            return View("Error");
        }
    }
}
