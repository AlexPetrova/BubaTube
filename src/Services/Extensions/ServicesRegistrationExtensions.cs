using Contracts.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Services.Contracts;
using Services.Contracts.Get;
using Services.Contracts.Write;
using Services.Get;
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

            services.AddTransient<IFileCommands, FileCommands>();
            services.AddTransient<IUserCommands, UserCommands>();
            services.AddTransient<IVideoCommands, VideoCommands>();
            services.AddTransient<ICategoryCommands, CategoryCommands>();

            services.AddDTOToModelMappers();
            services.AddModelToDTOMappers();

            services.AddSingleton(serviceProvider =>
                new Func<string, FileMode, FileStream>(
                    (name, mode) => new FileStream(name, mode)));

            services.AddSingleton(serviceProvider
                => new Func<string, string, PathInfo>((rootFolder, fileExtension) =>
                    {
                        var rootFolderName = "video";
                        var folder = Path.Combine(rootFolder, rootFolderName);
                        var nameOfVideo = Guid.NewGuid();
                        var path = Path.Combine(folder, nameOfVideo.ToString() + fileExtension);

                        return new PathInfo
                        {
                            Path = path,
                            FileName = $"{rootFolderName}/{nameOfVideo}{fileExtension}"
                        };
                    })
                );

            return services;
        }
    }
}
