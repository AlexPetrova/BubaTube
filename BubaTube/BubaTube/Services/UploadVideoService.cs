using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using BubaTube.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BubaTube.Services
{
    public class UploadVideoService : IUploadVideoService
    {
        private BubaTubeDbContext context;

        public UploadVideoService(BubaTubeDbContext context)
        {
            this.context = context;
        }

        public void SaveToDatabase(VideoDTO dto)
        {
            var categories = dto.Categories
                .Select(x => new Category()
                {
                    CategoryName = x,
                    VideoCategory = new List<VideoCategory>()
                })
                .ToList();

            this.context.Category.AddRange(categories);

            this.context.SaveChanges();

            var model = new Video()
            {
                Title = dto.Title,
                Description = dto.Description,
                Path = dto.Path,
                AuthorId = dto.AuthorId,
                VideoCategory = new List<VideoCategory>()
            };

            foreach (var category in categories)
            {
                model.VideoCategory.Add(new VideoCategory()
                {
                    CategoryId = category.Id
                });
            }

            this.context.Videos.Add(model);

            this.context.SaveChanges();
        }
        
    }
}
