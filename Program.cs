using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware to handle global exception handling
if (!app.Environment.IsDevelopment())
{
    // Use global exception handler for production environments
    app.UseExceptionHandler("/Error");  // Redirect to Error controller for unhandled exceptions
    app.UseStatusCodePagesWithReExecute("/Error/{0}"); // Handle HTTP status codes (404, 500, etc.)
}
else
{
    // Use Developer Exception Page for development environments
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
