using Contracts.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Services.Contracts;
using Services.Contracts.Get;
using Services.Contracts.Wrappers;
using Services.Contracts.Write;
using Services.Get;
using Services.Wrappers;
using Services.Write;
using System;
using System.IO;

namespace Services.Extensions
{
    public static class ServicesRegistrationExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICategoryQueries, CategoryQueries>();
            services.AddTransient<IFavouriteVideosQueries, FavouriteVideosQueries>();
            services.AddTransient<IVideoQueries, VideoQueries>();
            services.AddTransient<IUserQueries, UserQueries>();
            services.AddTransient<IFileQueries, FileQueries>();

            services.AddTransient<IFileCommands, FileCommands>();
            services.AddTransient<IUserCommands, UserCommands>();
            services.AddTransient<IVideoCommands, VideoCommands>();
            services.AddTransient<ICategoryCommands, CategoryCommands>();

            services.AddSingleton<IFile, FileWrapper>();

            services.AddDTOToModelMappers();
            services.AddModelToDTOMappers();

            services.AddSingleton(serviceProvider =>
                new Func<string, FileMode, Stream>(
                    (name, mode) => new FileStream(name, mode)));

            return services;
        }
    }
}
