using Contracts.Data.DTO;
using Contracts.Data.Models;
using DataAccess;
using Services.Contracts.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Get
{
    public class VideoQueries : IVideoQueries
    {
        private readonly BubaTubeDbContext context;
        private readonly Expression<Func<Video, VideoDTO>> videoMapper;
        private const int DefaultCountForResentVideos = 20;

        public VideoQueries(
            BubaTubeDbContext context,
            Expression<Func<Video, VideoDTO>> videoMapper)
        {
            this.context = context;
            this.videoMapper = videoMapper;
        }

        public IReadOnlyCollection<VideoDTO> MostResentVideos()
        {
            return this.context.Videos
                .Select(videoMapper)
                .Where(x => x.IsApproved == true && x.IsDeleted == false)
                .Take(DefaultCountForResentVideos)
                .ToList();
        }

        public IReadOnlyCollection<VideoDTO> PopularVideos()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<VideoDTO> UserMayLikeVideos(User user)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<VideoDTO> GetAllForApproval()
        {
            return this.context.Videos
                .Select(videoMapper)
                .Where(x => x.IsApproved == false && x.IsDeleted == false)
                .ToList();
        }
    }
}
