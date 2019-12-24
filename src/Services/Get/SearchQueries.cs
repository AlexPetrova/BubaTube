using Contracts.Data.DTO;
using DataAccess;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.Contracts.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Get
{
    public class SearchQueries : ISearchQueries
    {
        private readonly char[] splitChars = new char[] { ' ', '.', ',', '!', '?', '(', ')' };
        private const int MaxCountOfSearchedWords = 5;

        private readonly BubaTubeDbContext context;
        private readonly IConfiguration configuration;

        public SearchQueries(
            BubaTubeDbContext context,
            IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<string> GetSearchResultsJSON(string input)
        {
            var searchedValues = this.FormatSerachedInput(input);
            var result = await this.BuildJSON(searchedValues);

            return result;
        }

        public (IEnumerable<VideoDTO>, IEnumerable<CommentDTO>, IEnumerable<UserDTO>) GetQuickSearchResults(string input)
        {
            var serachedValues = this.FormatSerachedInput(input);

            var videos = this.SearchResultForVideos(serachedValues);
            var comments = this.SerachResultForComments(serachedValues);
            var users = this.SerachResultForUsers(serachedValues);

            return (videos, comments, users);
        }

        public IEnumerable<VideoDTO> SearchResultForVideos(IDictionary<string,string> input)
        {
            var result = this.context.Videos
                .Where(x => input.ContainsKey(x.Title.ToLower()))
                .Select(x =>
                    new VideoDTO()
                    {
                        Title = x.Title,
                        Likes = x.Likes
                    })
                .Take(5);

            return result;
        }
        
        public IEnumerable<CommentDTO> SerachResultForComments(IDictionary<string, string> input)
        {
            var result = this.context.Comments
                .Where(x => input.ContainsKey(x.User.FirstName.ToLower())
                    || input.ContainsKey(x.User.LastName.ToLower()))
                .Select(x =>
                    new CommentDTO()
                    {
                        Content = x.Content,
                        Likes = x.Likes,
                        UserId = x.UserId
                    })
                .Take(5);

            return result;
        }

        public IEnumerable<UserDTO> SerachResultForUsers(IDictionary<string, string> input)
        {
            var result = this.context.Users
                .Where(x =>
                    input.ContainsKey(x.FirstName.ToLower())
                    || input.ContainsKey(x.LastName.ToLower())
                    || input.ContainsKey(x.Email.ToLower()))
                .Select(x =>
                    new UserDTO()
                    {
                        FirstName =
                        x.FirstName,
                        LastName =
                        x.LastName,
                        Email = x.Email,
                        AvatarImage = x.AvatarImage
                    })
                .Take(5);

            return result;
        }

        private Task<string> BuildJSON(IDictionary<string, string> input)
        {
            var videos = this.context.Videos.
                Where(x => input.ContainsKey(x.Title.ToLower()));

            var users = this.context.Users.
                Where(x =>
                    input.ContainsKey(x.FirstName.ToLower())
                    || input.ContainsKey(x.LastName.ToLower())
                    || input.ContainsKey(x.Email.ToLower()));

            var videosToJson = JsonConvert.SerializeObject(videos.ToList());
            var usersToJson = JsonConvert.SerializeObject(users.ToList());

            var result = videosToJson + usersToJson;
            return Task.FromResult(result);
        }
        
        private Dictionary<string, string> FormatSerachedInput(string input)
        {
            var formated = input.
                Split(this.splitChars, StringSplitOptions.RemoveEmptyEntries).
                Select(x => x.ToLower()).
                Distinct().
                Take(MaxCountOfSearchedWords).
                ToDictionary(k => k, v => v);

            return formated;
        }
    }
}