﻿using Contracts.Data.DTO;
using Contracts.Data.Models;
using DataAccess;
using Services.Get;
using Services.Tests.MockData;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Services.Tests.Get
{
    public class FavouriteVideosQueries_Should
    {
        static readonly Expression<Func<Video, VideoDTO>> fakeMapper =
               v => new VideoDTO() { Title = v.Title };

        [Fact]
        public void ReturnsListOfFavouriteVideosOfUser()
        {
            var options = DbContextMock.GetOptions("ReturnsListOfFavouriteVideosOfUser");
            using (var context = new BubaTubeDbContext(options))
            {
                context.Users.Add(new User()
                {
                    UserName = "testUser"
                });
                context.Videos.Add(new Video()
                {
                    Title = "TestVideo",
                    FileName = "testVideo.mp4",
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
            var options = DbContextMock.GetOptions("ReturnsEmptyListIfNoFavouriteVideosSaved");
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
