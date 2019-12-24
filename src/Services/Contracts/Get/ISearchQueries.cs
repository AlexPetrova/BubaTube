using Contracts.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts.Get
{
    public interface ISearchQueries
    {
        (IEnumerable<VideoDTO>, IEnumerable<CommentDTO>, IEnumerable<UserDTO>) GetQuickSearchResults(string input);

        IEnumerable<VideoDTO> SearchResultForVideos(IDictionary<string, string> input);

        IEnumerable<UserDTO> SerachResultForUsers(IDictionary<string, string> input);

        IEnumerable<CommentDTO> SerachResultForComments(IDictionary<string, string> input);

        Task<string> GetSearchResultsJSON(string input);
    }
}
