using Contracts.Data.DTO;
using System.Collections.Generic;

namespace Services.Contracts.Get
{
    public interface IFavouriteVideosQueries
    {
        IReadOnlyCollection<VideoDTO> GetFavouriteVideos(UserSearchDTO user);
    }
}