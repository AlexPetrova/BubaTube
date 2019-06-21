using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using BubaTube.Helpers.Map;
using BubaTube.Services.Contracts.Get;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BubaTube.Services.GetServices
{
    public class VideoGetService : IVideoGetService
    {
        private BubaTubeDbContext context;
        private const int DefaultCountForResentVideos = 20;

        public VideoGetService(BubaTubeDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<VideoDTO> MostResentVideos()
        {
            var resentVideos = this.context.Videos
                .Where(x => x.IsАpproved == true)
                .TakeLast(DefaultCountForResentVideos)
                .Select(Map.Video())
                .ToList();

            return resentVideos;
        }

        public IEnumerable<VideoDTO> PopularVideos()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VideoDTO> UserMayLikeVideos(User user)
        {
            throw new NotImplementedException();
        }
    }
}
