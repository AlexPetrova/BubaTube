using BubaTube.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BubaTube.Services.Contracts
{
    public interface ISearchService
    {
        IEnumerable<VideoDTO> SearchResultForVideos(string input);

        IEnumerable<UserDTO> SerachResultForUsers(string input);

        IEnumerable<CommentDTO> SerachResultForComments(string input);

        Task<string> GetSearchResultsJSON(string input);
    }
}
