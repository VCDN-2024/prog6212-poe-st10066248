using ClaimsTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ClaimsTrackingSystem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult ViewClaims(string role)
        {
            // Get claims based on the role of the user (Coordinator or Manager)
            var claims = ClaimRepository.ClaimsList
                .Where(c => role == "Coordinator" ? c.Status == ClaimStatus.Pending : c.Status == ClaimStatus.Verified)
                .ToList();

            // If the role is Academic Manager, send all claims as history
            if (role == "Manager")
            {
                ViewData["History"] = ClaimRepository.ClaimsList.ToList();
            }

            ViewData["Role"] = role;
            return View(claims);
        }

        [HttpPost]
        public IActionResult VerifyClaim(int id, string role)
        {
            var claim = ClaimRepository.ClaimsList.FirstOrDefault(c => c.Id == id);
            if (claim != null && claim.Status == ClaimStatus.Pending)
            {
                claim.Status = ClaimStatus.Verified;
            }
            return RedirectToAction("ViewClaims", new { role = role });
        }

        [HttpPost]
        public IActionResult AcceptClaim(int id, string role)
        {
            var claim = ClaimRepository.ClaimsList.FirstOrDefault(c => c.Id == id);
            if (claim != null && claim.Status == ClaimStatus.Verified)
            {
                claim.Status = ClaimStatus.Accepted;
            }
            return RedirectToAction("ViewClaims", new { role = role });
        }

        [HttpPost]
        public IActionResult RejectClaim(int id, string role)
        {
            var claim = ClaimRepository.ClaimsList.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = ClaimStatus.Rejected;
            }
            return RedirectToAction("ViewClaims", new { role = role });
        }
    }
}


