using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace SmithSwimmingSchool.Data
{
    // Utility class to seed roles and an admin user
    public static class DataUtility
    {
        private static readonly string[] Roles = new[] { "Admin", "Coach", "Swimmer" };

        public static async Task InitializeAsync(IServiceProvider services)
        {
            // Ensure roles are created
            var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();
            // Create roles if they do not exist
            foreach (var r in Roles)
                // Check if role exists
                if (!await roleMgr.RoleExistsAsync(r))
                    await roleMgr.CreateAsync(new IdentityRole(r));

            // Ensure an admin user is created
            var userMgr = services.GetRequiredService<UserManager<IdentityUser>>();
            var email = Environment.GetEnvironmentVariable("SEED_ADMIN_EMAIL") ?? "admin@example.com";
            var pass = Environment.GetEnvironmentVariable("SEED_ADMIN_PASSWORD") ?? "Admin!234";

            // Check if the admin user already exists
            var admin = await userMgr.FindByEmailAsync(email);
            if (admin == null)
            {
                // Create the admin user
                admin = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
                // Create the user with the specified password
                var result = await userMgr.CreateAsync(admin, pass);
                // If creation succeeded, assign the Admin role
                if (result.Succeeded)
                {

                    await userMgr.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
