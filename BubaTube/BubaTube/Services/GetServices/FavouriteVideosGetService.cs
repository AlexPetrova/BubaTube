using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Helpers.Map;
using BubaTube.Services.Contracts.Get;
using System.Collections.Generic;
using System.Linq;

namespace BubaTube.Services.GetServices
{
    public class FavouriteVideosGetService : IFavouriteVideosGetService
    {
        private BubaTubeDbContext context;

        public FavouriteVideosGetService(BubaTubeDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<VideoDTO> GetFavouriteVideos(UserSearchDTO user)
        {
            var videos = this.context.UserVideo
                .Where(x => x.UserId == user.Id)
                .Select(x => x.Video)
                .Select(Map.Video());

            return videos;
        }
    }
}
