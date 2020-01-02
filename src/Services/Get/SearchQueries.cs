using Contracts.Data.DTO;
using DataAccess;
using Microsoft.EntityFrameworkCore;
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

        public async Task<string> GetJSON(string input)
        {
            var searchedValues = this.FormatSearchedInput(input);
            var result = await this.BuildJSON(searchedValues);

            return result;
        }

        public (IEnumerable<VideoDTO>, IEnumerable<CommentDTO>, IEnumerable<UserDTO>) GetQuickSearchResults(string input)
        {
            var serachedValues = this.FormatSearchedInput(input);

            var videos = this.Videos(serachedValues);
            var comments = this.Comments(serachedValues);
            var users = this.Users(serachedValues);

            return (videos, comments, users);
        }

        public IEnumerable<VideoDTO> Videos(IDictionary<string, string> input)
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

        public IEnumerable<CommentDTO> Comments(IDictionary<string, string> input)
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

        public IEnumerable<UserDTO> Users(IDictionary<string, string> input)
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

        private async Task<string> BuildJSON(IDictionary<string, string> input)
        {
            var videos = await this.context.Videos
                // TODO fix client-side evaluation
                //.Where(x => input.ContainsKey(x.Title.ToLower()))
                .ToListAsync();

            var users = await this.context.Users
                // TODO fix client-side evaluation
                //.Where(x =>
                //    input.ContainsKey(x.FirstName.ToLower())
                //    || input.ContainsKey(x.LastName.ToLower())
                //    || input.ContainsKey(x.Email.ToLower()));
                .ToListAsync();

            return JsonConvert.SerializeObject(new { videos, users });
        }

        private Dictionary<string, string> FormatSearchedInput(string input)
        {
            return input
                  .Split(this.splitChars, StringSplitOptions.RemoveEmptyEntries)
                  .Select(x => x.ToLower())
                  .Distinct()
                  .Take(MaxCountOfSearchedWords)
                  .ToDictionary(k => k, v => v);
        }
    }
}