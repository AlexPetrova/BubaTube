using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Services.Contracts.Get
{
    public interface IVideoGetService
    {
        IEnumerable<VideoDTO> MostResentVideos();

        IEnumerable<VideoDTO> PopularVideos();

        IEnumerable<VideoDTO> UserMayLikeVideos(User user);
    }
}
