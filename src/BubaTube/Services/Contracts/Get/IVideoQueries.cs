using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using System.Collections.Generic;

namespace BubaTube.Services.Contracts.Get
{
    public interface IVideoQueries
    {
        IReadOnlyCollection<VideoDTO> MostResentVideos();

        IReadOnlyCollection<VideoDTO> PopularVideos();

        IReadOnlyCollection<VideoDTO> UserMayLikeVideos(User user);
    }
}
