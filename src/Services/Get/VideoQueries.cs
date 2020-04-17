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
        private readonly Expression<Func<Video, VideoDTO>> dtoToModelMap;
        private readonly Expression<Func<Video, VideoPreviewDTO>> modelToDTOMap;
        private const int DefaultCountForResentVideos = 20;

        public VideoQueries(
            BubaTubeDbContext context,
            Expression<Func<Video, VideoDTO>> DTOToModelMap,
            Expression<Func<Video, VideoPreviewDTO>> ModelToDTOMap)
        {
            this.context = context;
            this.dtoToModelMap = DTOToModelMap;
            this.modelToDTOMap = ModelToDTOMap;
        }

        public IReadOnlyCollection<VideoPreviewDTO> MostResentVideos()
        {
            return this.context.Videos
                .Where(x => x.IsApproved == true && x.IsDeleted == false)
                .Take(DefaultCountForResentVideos)
                .Select(this.modelToDTOMap)
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
                .Where(x => x.IsApproved == false && x.IsDeleted == false)
                .Select(dtoToModelMap)
                .ToList();
        }
    }
}
