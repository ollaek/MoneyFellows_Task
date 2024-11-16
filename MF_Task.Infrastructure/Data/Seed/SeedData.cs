using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;


namespace MF_Task.Infrastructure.Data.Seed
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, IHostEnvironment env)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await CreateRoles(roleManager);

            await CreateAdminUser(userManager);
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = new string[] { "Admin", "User" };

            foreach (var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole { Name = role });
                    if (!roleResult.Succeeded)
                    {
                        throw new Exception($"Failed to create role {role}.");
                    }
                }
            }
        }

        private static async Task CreateAdminUser(UserManager<ApplicationUser> userManager)
        {
            var adminEmail = "admin@domain.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    PhoneNumber = "01012345678"
                };

                var createAdminResult = await userManager.CreateAsync(adminUser, "Admin123!");
                if (!createAdminResult.Succeeded)
                {
                    throw new Exception("Failed to create admin user.");
                }

                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}