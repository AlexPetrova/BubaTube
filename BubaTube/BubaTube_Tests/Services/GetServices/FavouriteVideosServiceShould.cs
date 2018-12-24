using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using BubaTube.Services.GetServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BubaTube_Tests.Services.GetServices
{
    public class FavouriteVideosServiceShould
    {
        [Fact]
        public void SouldReturnListOfVideosWhenPassedValidUser()
        {
            var options = this.GetOptions("SouldReturnListOfVideosWhenPassedValidUser");
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

                var favoutiteVideoService = new FavouriteVideosService();
                var userDto = new UserSearchDTO()
                {
                    Id = user.Id
                };

                var result = favoutiteVideoService.GetFavoriteUsers(userDto);

                Assert.NotEmpty(result);
                Assert.Single(result);
            }
        }
        [Fact]
        public void SouldReturnValidDataWhenPassedValidUser()
        {
            var options = this.GetOptions("SouldReturnValidDataWhenPassedValidUser");
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

                var favoutiteVideoService = new FavouriteVideosService();
                var userDto = new UserSearchDTO()
                {
                    Id = user.Id
                };

                var result = favoutiteVideoService.GetFavoriteUsers(userDto);

                Assert.Equal(video.Title, result.First().Title);
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
