using Contracts.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DataAccess.Extensions
{
    public static class ServicesRegistrationExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null)
        {
            services.AddDbContext<BubaTubeDbContext>(optionsAction);

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<BubaTubeDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
