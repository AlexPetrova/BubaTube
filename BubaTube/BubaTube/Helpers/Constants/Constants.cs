namespace BubaTube.Helpers.Constants
{
    public static class Constants
    {
        public const string VideosQuery =
            "SELECT Videos.Id, Videos.Likes, Videos.Path, Videos.AuthorId, Videos.Description, Videos.Title, Category.CategoryName " + 
            "FROM Videos " +
            "LEFT JOIN CategoryVideo ON Videos.Id = CategoryVideo.VideoId " + 
            "LEFT JOIN Category ON CategoryVideo.CategoryId = Category.Id " +
            "WHERE Videos.IsАpproved = 1 OR Category.IsАpproved = 1";

        public const string CommentsQuery = "SELECT * FROM Comments";

        public const string UsersQuery = "SELECT * FROM AspNetUsers";

        public const string VideosTable = "Videos";

        public const string UsersTable = "Users";

        public const string CommentsTable = "Comments";
    }
}
