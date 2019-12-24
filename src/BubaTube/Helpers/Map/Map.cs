using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BubaTube.Helpers.Map
{
    public static class Map
    {
        public static Func<User, UserDTO> User()
        {
            return x => new UserDTO()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                LastLogin = x.LastLogin,
                RegisteredOn = x.RegisteredOn,
                AvatarImage = x.AvatarImage
            };
        }

        public static Func<Video, VideoDTO> Video()
        {
            return x => new VideoDTO()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Path = x.Path,
                Likes = x.Likes,
                AuthorUserName = x.AuthorId,
                Categories = x.VideoCategory
                              .Select(c => c.Category.CategoryName)
            };
        }

        public static Video Video(VideoDTO videoDTO)
        {
            return new Video()
            {
                Title = videoDTO.Title,
                Description = videoDTO.Description,
                Path = videoDTO.Path,
                AuthorId = videoDTO.AuthorUserName,
                VideoCategory = new List<VideoCategory>()
            };
        }
    }
}
