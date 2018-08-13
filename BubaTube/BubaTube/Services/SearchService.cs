using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Factory;
using BubaTube.Helpers.JSON;
using BubaTube.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BubaTube.Services
{
    public class SearchService : ISearchService
    {
        private BubaTubeDbContext context;
        private JSONBuilder json;

        public SearchService(BubaTubeDbContext context)
        {
            this.context = context;
        }

        public string GetSearchResultsJSON(string input)
        {
            this.BuildJSON(input);
            return "";
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

        private void BuildJSON(string input)
        {
            var searchedValues = input.
                Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            this.json = HelperClassesFactory.CreateJSONBuilderInstance();

            this.json.AddJSONArray("Videos", this.GetVideosJSONObjects(searchedValues));
            this.json.AddJSONArray("Comments", this.GetCommentsJSONObjects(searchedValues));
        }

        private IEnumerable<JSONObject> GetCommentsJSONObjects(string[] searchedValues)
        {
            var commentJSONObjects = new List<JSONObject>();

            var comments = this.context.Comments.ToList();

            foreach (var comment in comments)
            {

                if (searchedValues.Contains(comment.Content))
                {
                    var json = HelperClassesFactory.CreateJSONObjectInstance();

                    json.AddProperty("IdInDB", comment.Id.ToString());
                    json.AddProperty("AuthorIDInDB", comment.UserId.ToString());
                    json.AddProperty("Content", comment.Content);
                    json.AddProperty("Likes", comment.Likes.ToString());

                    commentJSONObjects.Add(json);
                }
            }

            return commentJSONObjects;
        }

        private IEnumerable<JSONObject> GetVideosJSONObjects(string[] searchedValues)
        {
            var videoJSONObjects = new List<JSONObject>();

            var videos = this.context.Videos.ToList();

            foreach (var video in videos)
            {
                if (video.Tags.Intersect(searchedValues).Any())
                {
                    var json = HelperClassesFactory.CreateJSONObjectInstance();

                    json.AddProperty("IDInDB", video.Id.ToString());
                    json.AddProperty("AuthorIDInDB", video.AuthorId.ToString());
                    json.AddProperty("Likes", video.Likes.ToString());
                    json.AddProperty("Title", video.Title.ToString());
                    json.AddProperty("Tags", string.Join(',', video.Tags));

                    videoJSONObjects.Add(json);
                }
            }

            return videoJSONObjects;
        }

        private IEnumerable<JSONObject> GetUsersJSONObjects(string[] searchedValues)
        {

        }
    }
}
