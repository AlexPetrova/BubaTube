using Contracts.Extensions;
using DataAccess.Extensions;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddTransient<IFileCommands, FileCommands>();
            services.AddTransient<IUserCommands, UserCommands>();
            services.AddTransient<IVideoCommands, VideoCommands>();

            services.AddDTOToModelMappers();
            services.AddModelToDTOMappers();

            services.AddSingleton(serviceProvider => 
                new Func<string, FileMode, FileStream>(
                    (name, mode) => new FileStream(name, mode)));

            //TODO here should pass the db opions and it should be configurable somehow, this is not the best place for this registration - options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            services.AddDataAccess();

            services.AddSingleton(serviceProvider => new Func<string, string, string>((rootFolder, fileExtension) =>
            {
                var folder = Path.Combine(rootFolder, "video");
                var nameOfVideo = Guid.NewGuid();
                var path = Path.Combine(folder, nameOfVideo.ToString() + fileExtension);

                return path;
            }));

            return services;
        }
    }
}
