using Contracts.Data.DTO;
using Contracts.Data.Models;
using System.Collections.Generic;

namespace Services.Contracts.Get
{
    public interface IVideoQueries
    {
        IReadOnlyCollection<VideoPreviewDTO> MostResentVideos();

        IReadOnlyCollection<VideoDTO> PopularVideos();

        IReadOnlyCollection<VideoDTO> UserMayLikeVideos(User user);

        IReadOnlyCollection<VideoDTO> GetAllForApproval();
    }
}
