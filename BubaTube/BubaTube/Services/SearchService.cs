using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BubaTube.Services
{
    public class SearchService : ISearchService
    {
        private BubaTubeDbContext context;

        public SearchService(BubaTubeDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<VideoDTO> SearchResultForVideos(string input)
        {
            var result = this.context.Videos
                .TakeWhile(x => x.Title.Contains(input))
                .Select(x => new VideoDTO() { Title = x.Title, Likes = x.Likes })
                .Take(5);

            return result;
        }

        public IEnumerable<CommentDTO> SerachResultForComments(string input)
        {
            var result = this.context.Comments
                .TakeWhile(x => x.Content.Contains(input))
                .Select(x => new CommentDTO() { Content = x.Content, Likes = x.Likes, UserId = x.UserId })
                .Take(5);

            return result;
        }

        public IEnumerable<UserDTO> SerachResultForUsers(string input)
        {
            var result = this.context.Users
                .TakeWhile(x => x.FirstName.Contains(input) || x.LastName.Contains(input) || x.Email.Contains(input))
                .Select(x => new UserDTO() { FirstName = x.FirstName, LastName = x.LastName, Email = x.Email, AvatarImage = x.AvatarImage })
                .Take(5);

            return result;
        }
    }
}
