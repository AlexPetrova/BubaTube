using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using BubaTube.Factory.Contracts;
using BubaTube.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Services
{
    public class UploadVideoService : IUploadVideoService
    {
        private BubaTubeDbContext context;
        private IFileStreamFactory fileStreamFactory;

        public UploadVideoService(BubaTubeDbContext context, IFileStreamFactory fileStreamFactory)
        {
            this.context = context;
            this.fileStreamFactory = fileStreamFactory;
        }

        public void SaveToDatabase(VideoDTO dto)
        {
            this.SaveCategories(dto);

            var categorieIDs = this.TakeCategoryIds(dto.Categories);

            var model = new Video()
            {
                Title = dto.Title,
                Description = dto.Description,
                Path = dto.Path,
                AuthorId = dto.AuthorId,
                VideoCategory = new List<VideoCategory>()
            };

            foreach (var id in categorieIDs)
            {
                model.VideoCategory.Add(new VideoCategory()
                {
                    CategoryId = id
                });
            }

            this.context.Videos.Add(model);

            this.context.SaveChanges();
        }
        
        public async Task SaveToRootFolder(IFormFile video, string path)
        {
            using (var fileStream = this.fileStreamFactory.CreateFileStreamInstance(path, FileMode.Create))
            {
                await video.CopyToAsync(fileStream);
            }
        }

        private IEnumerable<Category> FilterCategories(IEnumerable<string> categories)
        {
            var categoryNamesSavedInDB = this.context.Category
                .Select(x => x.CategoryName)
                .ToList();

            var different = categories.Except(categoryNamesSavedInDB);

            var filtered = different.
                Select(x => new Category()
                {
                    CategoryName = x,
                    VideoCategory = new List<VideoCategory>()
                })
                .ToList();

            return filtered;
        }

        private void SaveCategories(VideoDTO dto)
        {
            var categories = this.FilterCategories(dto.Categories);

            this.context.Category.AddRange(categories);

            this.context.SaveChanges();
        }

        private IEnumerable<int> TakeCategoryIds(IEnumerable<string> categories)
        {
            var result = this.context.Category
                .ToList()
                .TakeWhile(x => categories.Contains(x.CategoryName))
                .Select(x => x.Id);

            return result;
        }
    }
}
