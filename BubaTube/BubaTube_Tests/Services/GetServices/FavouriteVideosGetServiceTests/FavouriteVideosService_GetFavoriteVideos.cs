using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using BubaTube.Services.GetServices;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace BubaTube_Tests.Services.GetServices.FavouriteVideosGetServiceTest
{
    public class FavouriteVideosService_GetFavoriteVideos
    {
        [Fact]
        public void ReturnsListOfFavouriteVideosOfUser()
        {
            var options = this.GetOptions("GetFavouriteVideosTest");
            using (var context = new BubaTubeDbContext(options))
            {
                context.Users.Add(new User()
                {
                    UserName = "testUser"
                });
                context.Videos.Add(new Video()
                {
                    Title = "TestVideo",
                    Path = "c://",
                    AuthorId = "123"
                });
                context.SaveChanges();

                var user = context.Users.First();
                var video = context.Videos.First();
                context.UserVideo.Add(new UserVideo()
                {
                    UserId = user.Id,
                    VideoId = video.Id
                });
                context.SaveChanges();

                var favoutiteVideoService = new FavouriteVideosGetService(context);
                var userDto = new UserSearchDTO()
                {
                    Id = user.Id
                };

                var result = favoutiteVideoService.GetFavouriteVideos(userDto);

                Assert.NotEmpty(result);
                Assert.Single(result);
                Assert.Equal(video.Title, result.First().Title);
            }
        }

        [Fact]
        public void ReturnsEmptyListIfNoFavouriteVideosSaved()
        {
            var options = this.GetOptions("GetFavouriteVideosTest");
            using (var context = new BubaTubeDbContext(options))
            {
                context.Users.Add(new User()
                {
                    UserName = "testUser"
                });
                context.SaveChanges();

                var user = context.Users.First();

                var favoutiteVideoService = new FavouriteVideosGetService(context);
                var userDto = new UserSearchDTO()
                {
                    Id = user.Id
                };

                var result = favoutiteVideoService.GetFavouriteVideos(userDto);

                Assert.Empty(result);
            }
        }

        private DbContextOptions<BubaTubeDbContext> GetOptions(string name)
        {
            return new DbContextOptionsBuilder<BubaTubeDbContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;
        }
    }
}
