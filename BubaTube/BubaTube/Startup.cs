using BubaTube.Data;
using BubaTube.Data.Models;
using BubaTube.Factory;
using BubaTube.Factory.Contracts;
using BubaTube.Helpers;
using BubaTube.Helpers.Contracts;
using BubaTube.Services;
using BubaTube.Services.Contracts;
using BubaTube.Services.Contracts.Get;
using BubaTube.Services.Contracts.Write;
using BubaTube.Services.GetServices;
using BubaTube.Services.WriteServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddDbContext<BubaTubeDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<BubaTubeDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddRouting(
                options => options.LowercaseUrls = true);
            services.AddMemoryCache();
            
            services.AddTransient<IVideoWriteService, VideoWriteService>();
            services.AddTransient<ICategoryWriteService, CategoryWriteService>();
            services.AddTransient<ICategoryGetService, CategoryGetService>();
            services.AddTransient<IJSONHelperFactory, JSONHelperFactory>();
            services.AddTransient<IFileStreamFactory, FileStreamFactory>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IUploadVideoHelper, UploadVideoHelper>();
            services.AddSingleton<IConfiguration>(Configuration);
        }
        private void RegisterAuthentication(IServiceCollection serviceCollection)
        {
            serviceCollection.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<BubaTubeDbContext>()
                .AddDefaultTokenProviders();
        }

        private void RegisterServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //this.SeedRolesAsync(app).Wait();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
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
