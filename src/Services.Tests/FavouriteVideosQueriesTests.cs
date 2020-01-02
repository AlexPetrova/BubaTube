using Contracts.Data.DTO;
using Contracts.Data.Models;
using DataAccess;
using Services.Get;
using Services.Tests.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Services.Tests
{
    public class FavouriteVideosQueriesTests
    {
        static readonly Func<Video, VideoDTO> fakeMapper =
               v => new VideoDTO() { Title = v.Title };

        [Fact]
        public void ReturnsListOfFavouriteVideosOfUser()
        {
            var options = DbContextMock.GetOptions("GetFavouriteVideosTest");
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

                var favoutiteVideoService = new FavouriteVideosQueries(context, fakeMapper);
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
            var options = DbContextMock.GetOptions("GetFavouriteVideosTest");
            using (var context = new BubaTubeDbContext(options))
            {
                context.Users.Add(new User()
                {
                    UserName = "testUser"
                });
                context.SaveChanges();

                var user = context.Users.First();

                var favoutiteVideoService = new FavouriteVideosQueries(context, fakeMapper);
                var userDto = new UserSearchDTO()
                {
                    Id = user.Id
                };

                var result = favoutiteVideoService.GetFavouriteVideos(userDto);

                Assert.Empty(result);
            }
        }
    }
}
