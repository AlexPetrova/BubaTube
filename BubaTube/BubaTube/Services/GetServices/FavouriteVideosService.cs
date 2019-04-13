using BubaTube.Data.DTO;
using BubaTube.Services.Contracts.Get;
using System;
using System.Collections.Generic;

namespace BubaTube.Services.GetServices
{
    public class FavouriteVideosGetService : IFavouriteVideosGetService
    {
        public IEnumerable<VideoDTO> GetFavouriteVideos(UserSearchDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
