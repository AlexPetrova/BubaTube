using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using System.Collections.Generic;

namespace BubaTube.Services.Contracts.Get
{
    public interface IVideoGetService
    {
        IEnumerable<VideoDTO> MostResentVideos();

        IEnumerable<VideoDTO> PopularVideos();

        IEnumerable<VideoDTO> UserMayLikeVideos(User user);
    }
}
