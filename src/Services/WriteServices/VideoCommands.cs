using Contracts.Data.DTO;
using Contracts.Data.Models;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Services.Contracts.Get;
using Services.Contracts.Write;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Services.WriteServices
{
    public class VideoCommands : IVideoCommands
    {
        private readonly BubaTubeDbContext context;
        private readonly IFileCommands fileCommands;
        private readonly ICategoryQueries categoryGetService;
        private readonly Func<VideoDTO, Video> videoMapper;

        // TODO register the mapping funcs from mapping extensions
        // TODO register the FileCommands 
        public VideoCommands(
            BubaTubeDbContext context,
            IFileCommands fileCommands,
            ICategoryQueries categoryGetService,
            Func<VideoDTO, Video> videoMapper)
        {
            this.context = context;
            this.fileCommands = fileCommands;
            this.categoryGetService = categoryGetService;
            this.videoMapper = videoMapper;
        }

        /// <summary>
        /// Save Video to Database and to disk
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<int> Save(VideoDTO dto, IFormFile video, string path)
        {
            var categorieIDs = this.categoryGetService.TakeCategoryIds(dto.Categories);

            var model = this.videoMapper(dto);

            model.VideoCategory = categorieIDs
                .Select(x => new VideoCategory() { CategoryId = x })
                .ToList();

            this.context.Videos.Add(model);

            var result = await this.context.SaveChangesAsync();

            await this.fileCommands.Save(video, path);

            return result;
        }
    }
}
