using Contracts.Data.DTO;
using DataAccess;
using Services.Contracts.Get;
using System.Collections.Generic;
using System.Linq;

namespace BubaTube.Services.GetServices
{
    public class FavouriteVideosQueries : IFavouriteVideosQueries
    {
        private readonly BubaTubeDbContext context;

        public FavouriteVideosQueries(BubaTubeDbContext context)
        {
            this.context = context;
        }

        public IReadOnlyCollection<VideoDTO> GetFavouriteVideos(UserSearchDTO user)
        {
            var videos = this.context.UserVideo
                .Where(x => x.UserId == user.Id)
                .Select(x => x.Video)
                .Select(Map.Video())
                .ToList();

            return videos;
        }
    }
}
