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

        public Video SaveToDatabase(VideoDTO dto)
        {
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

            return model;
        }
        
        public async Task SaveToRootFolder(IFormFile video, string path)
        {
            using (var fileStream = this.fileStreamFactory.CreateFileStreamInstance(path, FileMode.Create))
            {
                await video.CopyToAsync(fileStream);
            }
        }

        public IEnumerable<int> TakeCategoryIds(IEnumerable<string> categories)
        {
            var result = this.context.Category
                .Where(x => categories.Contains(x.CategoryName) && x.IsАpproved == true)
                .Select(x => x.Id)
                .ToList();

            return result;
        }
    }
}
