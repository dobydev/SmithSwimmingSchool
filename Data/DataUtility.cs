using Microsoft.AspNetCore.Identity;
using SmithSwimmingSchool.Models;

public static class DataUtility
{
    public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Ensure roles exist
        string[] roles = { "Admin", "Swimmer", "Coach" };
        // Create roles if they do not exist
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Seed Admin
        if (await userManager.FindByNameAsync("admin") == null)
        {
            // Create Admin user
            var admin = new ApplicationUser { UserName = "admin@3S.com", Email = "admin@3S.com" };
            await userManager.CreateAsync(admin, "Admin123!");
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        // Seed Swimmer
        if (await userManager.FindByNameAsync("swimmer") == null)
        {
            // Create Swimmer user
            var swimmer = new ApplicationUser { UserName = "swimmer@3S.com", Email = "swimmer@3S.com" };
            await userManager.CreateAsync(swimmer, "Swimmer123!");
            await userManager.AddToRoleAsync(swimmer, "Swimmer");
        }

        // Seed Coach
        if (await userManager.FindByNameAsync("coach") == null)
        {
            // Create Coach user
            var coach = new ApplicationUser { UserName = "coach@3s.com", Email = "coach@3s.com" };
            await userManager.CreateAsync(coach, "Coach123!");
            await userManager.AddToRoleAsync(coach, "Coach");
        }
    }
}