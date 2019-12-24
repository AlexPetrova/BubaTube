using BubaTube.Data.DTO;
using System.Collections.Generic;

namespace BubaTube.Services.Contracts.Get
{
    public interface IFavouriteVideosQueries
    {
        IReadOnlyCollection<VideoDTO> GetFavouriteVideos(UserSearchDTO user);
    }
}