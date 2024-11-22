using ClaimsTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Linq;

namespace ClaimsTrackingSystem.Controllers
{
    public class LecturerController : Controller
    {
        public IActionResult SubmitClaim()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitClaim(string lecturerName, string surname, string claimingForDate, int hoursWorked, int hourlyRate, string notes, IFormFile document, string communicationMethod, string contactInfo, string faculty)
        {
            try
            {
                // Parse the "Claiming For" date string (format: mm/yyyy)
                DateTime parsedClaimingForDate;
                if (!DateTime.TryParseExact("01/" + claimingForDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedClaimingForDate))
                {
                    throw new FormatException("Invalid date format. Please select a valid month.");
                }

                // Check if the claim is valid (not for the current month and <= 45 hours)
                if (parsedClaimingForDate >= DateTime.Now || hoursWorked > 45)
                {
                    throw new InvalidOperationException("Invalid claim: You cannot claim for the current month, and hours worked cannot exceed 45.");
                }

                // Save the uploaded file
                string documentPath = null;
                if (document != null)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    Directory.CreateDirectory(uploadsFolder);
                    documentPath = Path.Combine(uploadsFolder, document.FileName);

                    using (var fileStream = new FileStream(documentPath, FileMode.Create))
                    {
                        document.CopyTo(fileStream);
                    }
                }

                // Create new claim
                var newClaim = new Claim
                {
                    Id = ClaimRepository.ClaimsList.Count + 1,
                    LecturerName = lecturerName,
                    Surname = surname,
                    ClaimingForDate = parsedClaimingForDate,
                    HoursWorked = hoursWorked,
                    HourlyRate = hourlyRate,
                    Notes = notes,
                    DocumentPath = documentPath,
                    CommunicationMethod = communicationMethod,
                    ContactInfo = contactInfo,
                    Faculty = faculty,
                    Status = ClaimStatus.Pending
                };

                // Add to the repository
                ClaimRepository.ClaimsList.Add(newClaim);

                return RedirectToAction("TrackClaims");
            }
            catch (FormatException ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
            catch (InvalidOperationException ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
            catch (Exception ex) // Catch any other unexpected exceptions
            {
                // Log the exception details for debugging (optional)
                // e.g., Log(ex.Message);
                ViewData["Error"] = "An unexpected error occurred. Please try again.";
                return View();
            }
        }

        public IActionResult TrackClaims(string lecturerName = null)
        {
            try
            {
                var lecturerClaims = ClaimRepository.ClaimsList.ToList();

                if (!string.IsNullOrEmpty(lecturerName))
                {
                    lecturerClaims = lecturerClaims
                        .Where(c => c.LecturerName.ToLower().Contains(lecturerName.ToLower()))
                        .ToList();
                }

                ViewData["LecturerName"] = lecturerName;
                return View(lecturerClaims);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "An unexpected error occurred while fetching the claims. Please try again.";
                return View(new List<Claim>());
            }
        }
    }
}
