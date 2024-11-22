using Microsoft.AspNetCore.Mvc;

namespace ClaimsTrackingSystem.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index(string role)
        {
            // Pass the role to the view to identify if it's Manager or Coordinator
            ViewData["Role"] = role;
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate(string employeeCode, string password, string role)
        {
            // Check if the password is 'admin' (hardcoded for both roles)
            if (password != "admin")
            {
                ViewData["Error"] = "Invalid password.";
                ViewData["Role"] = role;
                return View("Index");
            }

            // Check if the employee code matches the role
            if (role == "Manager" && !employeeCode.StartsWith("0"))
            {
                ViewData["Error"] = "Sorry, you are not authorized for this role.";
                ViewData["Role"] = role;
                return View("Index");
            }
            else if (role == "Coordinator" && !employeeCode.StartsWith("1"))
            {
                ViewData["Error"] = "Sorry, you are not authorized for this role.";
                ViewData["Role"] = role;
                return View("Index");
            }

            // If the user is authenticated, redirect them to the appropriate claims page
            return RedirectToAction("ViewClaims", "Admin", new { role = role });
        }
    }
}
