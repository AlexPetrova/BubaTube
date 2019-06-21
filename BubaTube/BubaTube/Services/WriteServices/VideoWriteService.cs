using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using BubaTube.Factory.Contracts;
using BubaTube.Helpers.Map;
using BubaTube.Services.Contracts.Get;
using BubaTube.Services.Contracts.Write;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace BubaTube.Services.WriteServices
{
    public class VideoWriteService : IVideoWriteService
    {
        private BubaTubeDbContext context;
        private IFileStreamFactory fileStreamFactory;
        private ICategoryGetService categoryGetService;

        public VideoWriteService(
            BubaTubeDbContext context, 
            IFileStreamFactory fileStreamFactory,
            ICategoryGetService categoryGetService)
        {
            this.context = context;
            this.fileStreamFactory = fileStreamFactory;
            this.categoryGetService = categoryGetService;
        }

        public Video Save(VideoDTO dto)
        {
            var categorieIDs = this.categoryGetService.TakeCategoryIds(dto.Categories);

            var model = Map.Video(dto);

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
            using (var fileStream = this.fileStreamFactory.CreateFileStreamInstance(
                path, FileMode.Create))
            {
                await video.CopyToAsync(fileStream);
            }
        }
    }
}
