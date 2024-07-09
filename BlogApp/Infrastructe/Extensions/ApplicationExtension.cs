using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace BlogApp.Infrastructe.Extensions
{
    public static class ApplicationExtension
    {
        public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
        {
            // Create a scoped service provider to get the RepositoryContext instance.
            RepositoryContext context = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RepositoryContext>();

            // Check if there are pending migrations.
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate(); // Apply pending migrations to the database.
            }
        }
        public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
        {
            // Constants for admin user credentials
            const string adminUser = "Admin";
            const string adminPassword = "Passwd123*";

            UserManager<IdentityUser> userManager = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            RoleManager<IdentityRole> roleManager = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            IdentityUser user = await userManager.FindByNameAsync(adminUser);
            if (user is null)
            {
                user = new IdentityUser()
                {
                    Email = "admin@admin.com",
                    UserName = adminUser
                };
                var result = await userManager.CreateAsync(user, adminPassword);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Admin user could not be created: {errors}");
                }

                var roleResult = await userManager.AddToRolesAsync(user,
                    roleManager
                        .Roles
                        .Select(r => r.Name)
                        .ToList()
                );
                if (!roleResult.Succeeded) throw new Exception("Admin user could not created");

            }
        }
    }
}