using Contracts.Data.DTO;
using Contracts.Data.Models;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Services.Contracts.Get;
using Services.Contracts.Write;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Services.WriteServices
{
    public class VideoCommands : IVideoCommands
    {
        private readonly BubaTubeDbContext context;
        private readonly IFileStreamHelper fileStreamHelper;
        private readonly ICategoryQueries categoryGetService;

        public VideoCommands(
            BubaTubeDbContext context,
            IFileStreamHelper fileStreamHelper,
            ICategoryQueries categoryGetService)
        {
            this.context = context;
            this.fileStreamHelper = fileStreamHelper;
            this.categoryGetService = categoryGetService;
        }

        /// <summary>
        /// Save Video to Database and to disk
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<int> Save(VideoDTO dto, IFormFile video, string path)
        {
            var categorieIDs = this.categoryGetService.TakeCategoryIds(dto.Categories);

            var model = Map.Video(dto);

            model.VideoCategory = categorieIDs
                .Select(x => new VideoCategory() { CategoryId = x })
                .ToList();

            this.context.Videos.Add(model);

            var result = await this.context.SaveChangesAsync();

            await this.fileStreamHelper.SaveFile(video, path);

            return result;
        }
    }
}
