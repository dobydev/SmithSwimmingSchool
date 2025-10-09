/* Gabriel Doby, Gabe Armstrong, Baden Glass, Destini Liphart
   Dylan Medvik, Pavel Karima, Prashum  K C
   Group 1: Team Project
   CISS 411: Software Architecture with ASP.NET
   9/29/2025  */

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmithSwimmingSchool.Models;

var builder = WebApplication.CreateBuilder(args);


// Database connection string
var connection = $"Server=(localdb)\\mssqllocaldb;Database=SmithSwimmingSchoolDb;" +
    $"Trusted_Connection=True;MultipleActiveResultSets=true";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connection));

// Identity configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


// Seed initial data
using (var scope = app.Services.CreateScope())
{
    // Seed roles and users
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    // Get RoleManager
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    // Call the SeedUsersAsync method
    await DataUtility.SeedUsersAsync(userManager, roleManager);
}

app.Run();


