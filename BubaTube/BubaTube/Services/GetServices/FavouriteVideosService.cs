using BubaTube.Data.DTO;
using BubaTube.Services.Contracts.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Services.GetServices
{
    public class FavouriteVideosService : IFavouriteVideosService
    {
        public IEnumerable<VideoDTO> GetFavoriteUsers(UserSearchDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
