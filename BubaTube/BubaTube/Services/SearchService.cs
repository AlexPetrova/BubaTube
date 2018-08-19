using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Factory.Contracts;
using BubaTube.Helpers.Constants;
using BubaTube.Helpers.JSON;
using BubaTube.Services.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Services
{
    public class SearchService : ISearchService
    {
        private readonly char[] splitChars = new char[] { ' ', '.', ',', '!', '?', '(', ')' };

        private BubaTubeDbContext context;
        private IJSONHelperFactory factory;
        private IConfiguration configuration;

        public SearchService(
            BubaTubeDbContext context,
            IJSONHelperFactory factory, 
            IConfiguration configuration)
        {
            this.context = context;
            this.factory = factory;
            this.configuration = configuration;
        }

        public async Task<string> GetSearchResultsJSON(string input)
        {
            var result = await this.BuildJSON(input);
            return result;
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

        private async Task<string> BuildJSON(string input)
        {
            var searchedValues = input.
                Split(this.splitChars, StringSplitOptions.RemoveEmptyEntries).
                Select(x => x.ToLower()).
                ToArray();

            var jsonBuilder = this.factory.CreateJSONBuilderInstance();

            jsonBuilder.AddJSONArray("Videos", 
                await Task.Run(() => this.ReadTable(searchedValues, Constants.VideosQuery, this.ReadRow)));
            jsonBuilder.AddJSONArray("Comments", 
                await Task.Run(() => this.ReadTable(searchedValues, Constants.CommentsQuery, this.ReadRow)));
            jsonBuilder.AddJSONArray("Users", 
                await Task.Run(() => this.ReadTable(searchedValues, Constants.UsersQuery, this.ReadRow)));

            return jsonBuilder.ToString();
        }

        private IEnumerable<JSONObject> ReadTable(
            string[] searchedValues, 
            string query, 
            Func<IDataReader, string[], JSONObject> readerMethod)
        {
            var commentJSONObjects = new List<JSONObject>();

            var connectionString = this.configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand(query, connection);
                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var currentRow = readerMethod(reader, searchedValues);

                    commentJSONObjects.Add(currentRow);
                }

                connection.Close();
            }

            return commentJSONObjects;
        }

        private JSONObject ReadRow(IDataReader reader, string[] searchedValues)
        {
            var jsonObject = this.factory.CreateJSONObjectInstance();

            var rowValues = new Dictionary<string, string>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var colName = reader.GetName(i);
                var value = reader[i].ToString();
                rowValues.Add(colName, value);
            }

            var values = rowValues.
                Select(x => x.Value.ToLower()).
                ToArray();

            if (searchedValues.Intersect(values).Any())
            {
                foreach (var val in rowValues)
                {
                    jsonObject.AddProperty(val.Key, val.Value);
                }
            }

            return jsonObject;
        }
    }
}