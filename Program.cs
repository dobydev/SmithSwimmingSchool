/* Gabriel Doby, Gabe Armstrong, Baden Glass, Destini Liphart
   Dylan Medvik, Pavel Karima, Prashum  K C
   Group 1: Team Project
   CISS 411: Software Architecture with ASP.NET
   9/29/2025  */

using Microsoft.EntityFrameworkCore;
using SmithSwimmingSchool.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// Database connection string
var connection = $"Server=(localdb)\\mssqllocaldb;Database=3SDb;" +
    $"Trusted_Connection=True;MultipleActiveResultSets=true";
builder.Services.AddDbContext<SmithDbContext>(options =>
               options.UseSqlServer(connection));

var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();

app.Run();


