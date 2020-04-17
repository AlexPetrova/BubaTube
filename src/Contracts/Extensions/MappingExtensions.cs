using Contracts.Data.DTO;
using Contracts.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Contracts.Extensions
{
    public static class MappingExtensions
    {
        public static IServiceCollection AddModelToDTOMappers(this IServiceCollection services)
        {
            services.AddSingleton<Expression<Func<User, UserDTO>>>(
                _ => model =>
                   new UserDTO
                   {
                       FirstName = model.FirstName,
                       LastName = model.LastName,
                       Email = model.Email,
                       LastLogin = model.LastLogin,
                       RegisteredOn = model.RegisteredOn,
                       AvatarImage = model.AvatarImage
                   });

            services.AddSingleton<Expression<Func<Video, VideoDTO>>>(
                _ => model =>
                    new VideoDTO
                    {
                        Id = model.Id,
                        Title = model.Title,
                        Description = model.Description,
                        FileName = model.FileName,
                        Likes = model.Likes,
                        AuthorUserId = model.AuthorId,
                        Categories = model.VideoCategory
                                          .Select(c => c.Category.CategoryName)
                    }
            );

            services.AddSingleton(serviceProvider =>
                new Func<Category, CategoryDTO>(model =>
                    new CategoryDTO
                    {
                        Id = model.Id,
                        CategoryName = model.CategoryName
                    }
                )
            );

            services.AddSingleton<Expression<Func<Video, VideoPreviewDTO>>>(serviceProvider =>
                model =>
                    new VideoPreviewDTO
                    {

                        Id = model.Id,
                        Title = model.Title,
                        FileName = model.FileName,
                        Author = new UserPreviewDTO
                        {
                            Id = model.AuthorId,
                            FirstName = model.Author.FirstName,
                            LastName = model.Author.LastName,
                            ProfilePicture = model.Author.AvatarImage
                        }
                    }
            );

            return services;
        }

        public static IServiceCollection AddDTOToModelMappers(this IServiceCollection services)
        {
            services.AddSingleton(serviceProvider =>
                new Func<VideoDTO, Video>((dto) =>
                    new Video
                    {
                        Title = dto.Title,
                        Description = dto.Description,
                        FileName = dto.FileName,
                        AuthorId = dto.AuthorUserId,
                        VideoCategory = new List<VideoCategory>()
                    }
                )
            );

            return services;
        }
    }
}
