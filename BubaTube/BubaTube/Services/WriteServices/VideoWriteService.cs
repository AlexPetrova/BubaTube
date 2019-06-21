using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using BubaTube.Factory.Contracts;
using BubaTube.Helpers.Constants;
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
        private IFileStreamHelper fileStreamHelper;
        private ICategoryGetService categoryGetService;

        public VideoWriteService(
            BubaTubeDbContext context,
            IFileStreamHelper fileStreamHelper,
            ICategoryGetService categoryGetService)
        {
            this.context = context;
            this.fileStreamHelper = fileStreamHelper;
            this.categoryGetService = categoryGetService;
        }

        /// <summary>
        /// Save Video to Database
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Saves <see cref="IFormFile"/> to path on disk
        /// </summary>
        /// <param name="video"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task Save(IFormFile video, string path)
        {
            await this.fileStreamHelper.SaveFile(video, path);
        }
    }
}
