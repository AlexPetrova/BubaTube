using BubaTube.Data.DTO;
using System.Collections.Generic;

namespace BubaTube.Services.Contracts.Get
{
    public interface IFavouriteVideosService
    {
        IEnumerable<VideoDTO> GetFavouriteVideos(UserSearchDTO user);
    }
}
