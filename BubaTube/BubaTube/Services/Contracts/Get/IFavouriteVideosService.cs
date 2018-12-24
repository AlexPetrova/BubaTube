using BubaTube.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Services.Contracts.Get
{
    public interface IFavouriteVideosService
    {
        IEnumerable<VideoDTO> GetFavoriteUsers(UserSearchDTO user);
    }
}
