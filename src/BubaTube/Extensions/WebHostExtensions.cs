using Contracts.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BubaTube.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost SeedRoles(this IWebHost host)
        {
            const string AdminRole = "Admin";
            const string AdminUsername = "AdminUser@abv.bg";
            const string AdminPassword = "Admin1@";

            Func<Task> seedFn = async () =>
            {
                using (var serviceScope = host.Services.CreateScope())
                {
                    var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                    var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                    if (!(await roleManager.RoleExistsAsync(AdminRole)))
                    {
                        await roleManager.CreateAsync(new IdentityRole() { Name = AdminRole });
                    }

                    if ((await userManager.FindByNameAsync(AdminUsername)) == null)
                    {
                        var adminUser = new User()
                        {
                            Email = AdminUsername,
                            UserName = AdminUsername,
                            FirstName = "Alexandra",
                            LastName = "Petrova"
                        };

                        var result = await userManager.CreateAsync(adminUser);

                        if (result.Succeeded)
                        {
                            await userManager.AddPasswordAsync(adminUser, AdminPassword);
                            await userManager.AddToRoleAsync(adminUser, AdminRole);
                        }
                    }
                }
            };

            seedFn().GetAwaiter().GetResult();

            return host;
        }
    }
}
