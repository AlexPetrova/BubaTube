using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Factory;
using BubaTube.Factory.Contracts;
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
        private IJSONHelperFactory factory;

        public SearchService(BubaTubeDbContext context, IJSONHelperFactory factory)
        {
            this.context = context;
            this.factory = factory;
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

            var jsonBuilder = this.factory.CreateJSONBuilderInstance();

            jsonBuilder.AddJSONArray("Videos", this.GetVideosJSONObjects(searchedValues));
            jsonBuilder.AddJSONArray("Comments", this.GetCommentsJSONObjects(searchedValues));
        }

        private IEnumerable<JSONObject> GetCommentsJSONObjects(string[] searchedValues)
        {
            var commentJSONObjects = new List<JSONObject>();

            var comments = this.context.Comments.ToList();

            foreach (var comment in comments)
            {
                if (searchedValues.Contains(comment.Content))
                {
                    var jsonObject = this.factory.CreateJSONObjectInstance();

                    jsonObject.AddProperty("IdInDB", comment.Id.ToString());
                    jsonObject.AddProperty("AuthorIDInDB", comment.UserId.ToString());
                    jsonObject.AddProperty("Content", comment.Content);
                    jsonObject.AddProperty("Likes", comment.Likes.ToString());

                    commentJSONObjects.Add(jsonObject);
                }
            }

            return commentJSONObjects;
        }

        private IEnumerable<JSONObject> GetVideosJSONObjects(string[] searchedValues)
        {
            var videosJSONObjects = new List<JSONObject>();

            var videos = this.context.Videos.ToList();

            foreach (var video in videos)
            {
                if (video.Tags.Intersect(searchedValues).Any())
                {
                    var jsonObject = this.factory.CreateJSONObjectInstance();

                    jsonObject.AddProperty("IDInDB", video.Id.ToString());
                    jsonObject.AddProperty("AuthorIDInDB", video.AuthorId.ToString());
                    jsonObject.AddProperty("Likes", video.Likes.ToString());
                    jsonObject.AddProperty("Title", video.Title.ToString());
                    jsonObject.AddProperty("Tags", string.Join(',', video.Tags));

                    videosJSONObjects.Add(jsonObject);
                }
            }

            return videosJSONObjects;
        }

        private IEnumerable<JSONObject> GetUsersJSONObjects(string[] searchedValues)
        {
            throw new NotImplementedException();
        }
    }
}
