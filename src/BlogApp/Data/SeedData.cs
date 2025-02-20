using BlogApp.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var adminRole = new IdentityRole("Admin");
                if (!await roleManager.RoleExistsAsync("Admin"))
                    await roleManager.CreateAsync(adminRole);

                string adminEmail = "admin@blog.com";
                string adminPassword = "Admin123!";

                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var admin = new AppUser { UserName = adminEmail, Email = adminEmail };
                    await userManager.CreateAsync(admin, adminPassword);
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
