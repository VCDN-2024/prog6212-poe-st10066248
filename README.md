    Visual Studio: Install Visual Studio 2022 with the ASP.NET and web development workload.

Steps

    Clone the Repository:

git clone https://github.com/your-repository/claims-management-system.git
cd claims-management-system

Install Dependencies:

    Open the project in Visual Studio.
    Install the required NuGet packages:
        Microsoft.EntityFrameworkCore
        Microsoft.EntityFrameworkCore.SqlServer
        Microsoft.EntityFrameworkCore.Tools
        iText7

Use the Package Manager Console:

dotnet restore

Configure Database:

    Open the appsettings.json file and update the connection string:

    "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ClaimsTrackingSystem;Trusted_Connection=True;"
    }

Apply Migrations:

    Run the following commands to create the database:

    dotnet ef migrations add InitialCreate
    dotnet ef database update

Run the Application:

    Start the application:

        dotnet run

        Open your browser and navigate to https://localhost:5001.

Usage
Home Page

    Select Role: Choose Lecturer, HR, Academic Manager, or Program Coordinator.

Lecturer

    Submit and track claims.

HR Module

    Access the HR Dashboard for managing lecturers and generating approved claims reports.

Manager/Coordinator

    Review and process claims (Approve/Reject).

Folder Structure

    Controllers: Contains the controllers (HRController, LecturerController, etc.).
    Models: Contains data models (Claim, Lecturer).
    Views: Contains Razor Views (HR, Lecturer, etc.).
    Data: Contains ApplicationDbContext for database interaction.
    Migrations: Tracks EF Core migrations.

Key Endpoints
Role	Endpoint	Description
Home Page	/	Landing page with role selection.
Submit Claim	/Lecturer/SubmitClaim	Form for claim submission.
Track Claims	/Lecturer/TrackClaims	View submitted claims and statuses.
HR Dashboard	/HR/Index	Main HR page for navigation.
Manage Lecturers	/HR/ManageLecturers	CRUD operations for lecturers.
Approved Claims	/HR/ApprovedClaimsReport	View approved claims.
Download Report	/HR/DownloadApprovedClaimsReport	Download claims report as PDF.
Features to Expand

    Authentication:
        Add user authentication for role-based access.
    Email Notifications:
        Notify lecturers when claims are approved or rejected.
    Enhanced Reporting:
        Include filters (e.g., by month, lecturer) in reports.

Troubleshooting
Common Issues

    Database Not Found:
        Ensure you’ve run the migrations (dotnet ef database update).
        Verify the connection string in appsettings.json.

    PDF Not Downloading:
        Ensure iText7 is installed (dotnet add package iText7).
        Check the HRController method for generating the PDF.

    404 Error:
        Verify the controller routes and view file names.
        Ensure proper navigation links in _Layout.cshtml.

License

This project is licensed under the MIT License.
