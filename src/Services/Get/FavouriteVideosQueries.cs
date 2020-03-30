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
    public class FavouriteVideosQueries : IFavouriteVideosQueries
    {
        private readonly BubaTubeDbContext context;
        private readonly Expression<Func<Video, VideoDTO>> videoMapper;

        public FavouriteVideosQueries(
            BubaTubeDbContext context,
            Expression<Func<Video, VideoDTO>> videoMapper)
        {
            this.context = context;
            this.videoMapper = videoMapper;
        }

        public IReadOnlyCollection<VideoDTO> GetFavouriteVideos(UserSearchDTO user)
        {
            var videos = this.context.UserVideo
                .Where(x => x.UserId == user.Id)
                .Select(x => x.Video)
                .Select(this.videoMapper)
                .ToList();

            return videos;
        }
    }
}
