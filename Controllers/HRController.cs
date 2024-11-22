using ClaimsTrackingSystem.Data;
using ClaimsTrackingSystem.Models;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using iText.Layout;
using System.IO;
using System.Linq;

namespace ClaimsTrackingSystem.Controllers
{
    public class HRController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HRController(ApplicationDbContext context)
        {
            _context = context;
        }

        // HR Dashboard
        public IActionResult Index()
        {
            return View();
        }

        // Manage Lecturers
        public async Task<IActionResult> ManageLecturers()
        {
            var lecturers = await _context.Lecturers.ToListAsync();
            return View(lecturers);
        }

        // Approved Claims Report
        public async Task<IActionResult> ApprovedClaimsReport()
        {
            var claims = await _context.Claims
                .Where(c => c.Status == ClaimStatus.Accepted)
                .ToListAsync();
            return View(claims);
        }

        // Lecturer Details
        public async Task<IActionResult> LecturerDetails(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null) return NotFound();
            return View(lecturer);
        }

        // Create Lecturer
        public IActionResult CreateLecturer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLecturer(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageLecturers));
            }
            return View(lecturer);
        }

        // Edit Lecturer
        public async Task<IActionResult> EditLecturer(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null) return NotFound();
            return View(lecturer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLecturer(int id, Lecturer lecturer)
        {
            if (id != lecturer.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecturer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Lecturers.Any(e => e.Id == id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(ManageLecturers));
            }
            return View(lecturer);
        }

        // Delete Lecturer
        public async Task<IActionResult> DeleteLecturer(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null) return NotFound();

            _context.Lecturers.Remove(lecturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageLecturers));
        }

        // Generate and download the Approved Claims Report as a PDF
        public IActionResult DownloadApprovedClaimsReport()
        {
            // Fetch the approved claims
            var claims = _context.Claims
                .Where(c => c.Status == ClaimStatus.Accepted)
                .ToList();

            // Create a memory stream to store the PDF
            using (var memoryStream = new MemoryStream())
            {
                // Initialize the PDF writer and document
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                // Add the title
                document.Add(new Paragraph("Approved Claims Report")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(18)
                    .SetBold());

                // Add a table with headers
                var table = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(5)).UseAllAvailableWidth();
                table.AddHeaderCell("Lecturer Name");
                table.AddHeaderCell("Hours Worked");
                table.AddHeaderCell("Hourly Rate");
                table.AddHeaderCell("Payout");
                table.AddHeaderCell("Approval Date");

                // Populate the table with data
                foreach (var claim in claims)
                {
                    table.AddCell(claim.LecturerName);
                    table.AddCell(claim.HoursWorked.ToString());
                    table.AddCell(claim.HourlyRate.ToString("C"));
                    table.AddCell((claim.HoursWorked * claim.HourlyRate).ToString("C"));
                }

                document.Add(table);

                // Close the document
                document.Close();

                // Return the PDF as a downloadable file
                return File(memoryStream.ToArray(), "application/pdf", "ApprovedClaimsReport.pdf");
            }
        }
    }
}
