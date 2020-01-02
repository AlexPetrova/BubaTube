using Contracts.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BubaTube
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRouting(
                options => options.LowercaseUrls = true);
            services.AddMemoryCache();

            // TODO 

            //services.AddServices();
            //services.AddDataAccess()


            services.AddSingleton<IConfiguration>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //this.SeedRolesAsync(app).Wait();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.Run(async context => { context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = 100_000_000; });
        }

        public async Task SeedRolesAsync(IApplicationBuilder app)
        {
            const string AdminRole = "Admin";
            const string AdminUsername = "AdminUser@abv.bg";
            const string AdminPassword = "Admin1@";

            using (var serviceScope = app.ApplicationServices.CreateScope())
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
        }
    }
}
