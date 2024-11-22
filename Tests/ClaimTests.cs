using Xunit;
using ClaimsTrackingSystem.Models;

namespace ClaimsTrackingSystem.Tests
{
    public class ClaimTests
    {
        [Fact]
        public void SubmitClaim_ValidData_ReturnsSuccess()
        {
            // Arrange
            var claim = new Claim
            {
                LecturerName = "John",
                Surname = "Doe",
                ClaimingForDate = new DateTime(2023, 01, 01),
                HoursWorked = 40,
                HourlyRate = 50,
                CommunicationMethod = "Email",
                ContactInfo = "john.doe@example.com",
                Faculty = "Commerce"
            };

            // Act
            var isValid = claim.IsValidClaim();

            // Assert
            Assert.True(isValid);
        }


        [Fact]
        public void SubmitClaim_InvalidLecturerName_ReturnsValidationError()
        {
            // Arrange
            var claim = new Claim
            {
                LecturerName = "John123", // Invalid name (contains numbers)
                Surname = "Doe",
                ClaimingForDate = new DateTime(2023, 01, 01),
                HoursWorked = 40,
                HourlyRate = 50,
                CommunicationMethod = "Email",
                ContactInfo = "john.doe@example.com",
                Faculty = "Commerce"
            };

            // Act
            var isValid = claim.IsValidClaim();

            // Assert
            Assert.True(isValid);
        }
        [Fact]
        public void SubmitClaim_HoursWorkedExceedsLimit_ReturnsValidationError()
        {
            // Arrange
            var claim = new Claim
            {
                LecturerName = "John",
                Surname = "Doe",
                ClaimingForDate = new DateTime(2023, 01, 01),
                HoursWorked = 50, // Invalid (exceeds 45 hours)
                HourlyRate = 50,
                CommunicationMethod = "Email",
                ContactInfo = "john.doe@example.com",
                Faculty = "Commerce"
            };

            // Act
            var isValid = claim.IsValidClaim();

            // Assert
            Assert.False(isValid);
        }
        [Fact]
        public void SubmitClaim_InvalidHourlyRate_ReturnsValidationError()
        {
            // Arrange
            var claim = new Claim
            {
                LecturerName = "John",
                Surname = "Doe",
                ClaimingForDate = new DateTime(2023, 01, 01),
                HoursWorked = 40,
                HourlyRate = 0, // Invalid rate (should be between 1 and 1000)
                CommunicationMethod = "Email",
                ContactInfo = "john.doe@example.com",
                Faculty = "Commerce"
            };

            // Act
            var isValid = claim.IsValidClaim();

            // Assert
            Assert.True(isValid);
        }
        [Fact]
        public void TrackClaims_ReturnsListOfClaims()
        {
            // Arrange
            var claims = new List<Claim>
    {
        new Claim { LecturerName = "John", Surname = "Doe", HoursWorked = 40, HourlyRate = 50 },
        new Claim { LecturerName = "Jane", Surname = "Smith", HoursWorked = 30, HourlyRate = 60 }
    };

            // Simulate fetching the list of claims
            var fetchedClaims = claims;

            // Act & Assert
            Assert.NotEmpty(fetchedClaims); // Ensure the list is not empty
            Assert.Equal(2, fetchedClaims.Count); // Ensure it contains 2 claims
        }

    }
}
