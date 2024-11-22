using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ClaimsTrackingSystem.Models
{
    public enum ClaimStatus
    {
        Pending,
        Verified,
        Accepted,
        Rejected
    }

    public enum CommunicationMethod
    {
        Email,
        SMS
    }

    public enum Faculty
    {
        Commerce,
        ScienceAndTechnology,
        Law,
        Education,
        Humanities
    }

    public class Claim
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lecturer Name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Lecturer Name must only contain letters.")]
        public string LecturerName { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Surname must only contain letters.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Claiming For date is required.")]
        public DateTime ClaimingForDate { get; set; } // Already handled by the date picker

        [Required(ErrorMessage = "Hours Worked is required.")]
        [Range(1, 45, ErrorMessage = "Hours Worked must be between 1 and 45.")]
        public int HoursWorked { get; set; }

        [Required(ErrorMessage = "Hourly Rate is required.")]
        [Range(1, 1000, ErrorMessage = "Hourly Rate must be between 1 and 1000.")]
        public int HourlyRate { get; set; }

        public string Notes { get; set; }
        public string DocumentPath { get; set; }

        [Required(ErrorMessage = "Please select a communication method.")]
        public string CommunicationMethod { get; set; }

        [Required(ErrorMessage = "Contact Information is required.")]
        public string ContactInfo { get; set; }

        [Required(ErrorMessage = "Please select a faculty.")]
        public string Faculty { get; set; }

        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;

        public double Payout => HoursWorked * HourlyRate;

        public string DocumentFileName
        {
            get
            {
                if (!string.IsNullOrEmpty(DocumentPath))
                {
                    return Path.GetFileName(DocumentPath);
                }
                return null;
            }
        }

        public bool IsValidClaim()
        {
            // Ensure the claim is not for the current month and hours worked <= 45
            if (ClaimingForDate >= DateTime.Now || HoursWorked > 45)
            {
                return false;
            }
            return true;
        }
    }

    public static class ClaimRepository
    {
        public static List<Claim> ClaimsList { get; set; } = new List<Claim>();
    }
}

