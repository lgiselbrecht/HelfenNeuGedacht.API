using HelfenNeuGedacht.API.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace HelfenNeuGedacht.API.Infrastructure.Seed
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles =
            {
                Roles.User,
                Roles.OrganizationUser,
                Roles.OrganizationAdmin,
                Roles.SuperAdmin
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    Console.WriteLine($"Role '{role}' created.");
                }
            }
        }
    }
}