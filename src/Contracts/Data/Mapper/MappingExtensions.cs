using Contracts.Data.DTO;
using Contracts.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contracts.Data.Mapper
{
    public static class MappingExtensions
    {
        public static IServiceCollection AddModelToDTOMappers(this IServiceCollection services)
        {
            services.AddSingleton(serviceProvider =>
                new Func<User, UserDTO>((model) =>
                {
                    return new UserDTO
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        LastLogin = model.LastLogin,
                        RegisteredOn = model.RegisteredOn,
                        AvatarImage = model.AvatarImage
                    };
                }
            ));

            services.AddSingleton(serviceProvider =>
                new Func<Video, VideoDTO>((model) =>
                {
                    return new VideoDTO
                    {
                        Id = model.Id,
                        Title = model.Title,
                        Description = model.Description,
                        Path = model.Path,
                        Likes = model.Likes,
                        AuthorUserName = model.AuthorId,
                        Categories = model.VideoCategory
                                          .Select(c => c.Category.CategoryName)
                    };
                }
            ));

            return services;
        }

        public static IServiceCollection AddDTOToModelMappers(this IServiceCollection services)
        {
            services.AddSingleton(serviceProvider =>
                new Func<VideoDTO, Video>((dto) =>
                {
                    return new Video
                    {
                        Title = dto.Title,
                        Description = dto.Description,
                        Path = dto.Path,
                        AuthorId = dto.AuthorUserName,
                        VideoCategory = new List<VideoCategory>()
                    };
                }
            ));

            return services;
        }
    }
}
