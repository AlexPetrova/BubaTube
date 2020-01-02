using Contracts.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts.Get
{
    public interface ISearchQueries
    {
        (IEnumerable<VideoDTO>, IEnumerable<CommentDTO>, IEnumerable<UserDTO>) GetQuickSearchResults(string input);

        IEnumerable<VideoDTO> Videos(IDictionary<string, string> input);

        IEnumerable<UserDTO> Users(IDictionary<string, string> input);

        IEnumerable<CommentDTO> Comments(IDictionary<string, string> input);

        Task<string> GetJSON(string input);
    }
}
