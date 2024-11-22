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

License

This project is licensed under the MIT License.
