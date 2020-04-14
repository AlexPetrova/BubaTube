using BubaTube_Tests.MockData;
using Contracts.Data.DTO;
using Contracts.Data.Models;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Moq;
using Services.Contracts.Get;
using Services.Contracts.Write;
using Services.Tests.MockData;
using Services.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Services.Tests.Write
{
    public class VideoCommands_Should
    {
        static readonly Func<VideoDTO, Video> fakeMapper = _ => new Video();

        [Fact]
        public async Task SavesPassedData()
        {
            var options = DbContextMock.GetOptions("SavesPassedData");
            var mockFile = new Mock<IFormFile>();
            var mockFileCommands = new Mock<IFileCommands>();
            var mockCategoryQueries = new Mock<ICategoryQueries>();
            Func<VideoDTO, Video> fakeMapper = _ =>
                new Video
                {
                    Title = "testVideo",
                    Description = "test",
                    Likes = 1,
                    Path = @"\Folder\Name.mp4"
                };
            var model = this.GetVideoDto();
            var categories = new List<string>();

            mockCategoryQueries
                .Setup(mock => mock.TakeAllCategoryIds(categories))
                .Returns(new List<int>() { 2 });

            using (var actContext = new BubaTubeDbContext(options))
            {
                var uploadService = new VideoCommands(
                    actContext, mockFileCommands.Object, mockCategoryQueries.Object, fakeMapper);

                await uploadService.Save(model, mockFile.Object);
            }

            using (var assertContext = new BubaTubeDbContext(options))
            {
                var savedModelInDb = assertContext.Videos.First();

                Assert.Equal(1, assertContext.Videos.Count());
                Assert.Equal(model.Title, savedModelInDb.Title);
                Assert.Equal(model.Description, savedModelInDb.Description);
                Assert.Equal(model.Path, savedModelInDb.Path);
            }
        }

        [Fact]
        public async Task AddsListOfCategories()
        {
            var options = DbContextMock.GetOptions("AddsListOfCategories");
            var mockFile = new Mock<IFormFile>();
            var mockFileCommands = new Mock<IFileCommands>();
            var mockCategoryQueries = new Mock<ICategoryQueries>();
            var categories = new List<string>() { "Test1" };

            mockCategoryQueries
                .Setup(mock => mock.TakeAllCategoryIds(categories))
                .Returns(new List<int>() { 2 });

            using (var actContext = new BubaTubeDbContext(options))
            {
                var model = this.GetVideoDto();
                model.Categories = categories;

                var uploadVideoService = new VideoCommands(
                    actContext, mockFileCommands.Object, mockCategoryQueries.Object, fakeMapper);

                await uploadVideoService.Save(model, mockFile.Object);
            }

            using (var assertContext = new BubaTubeDbContext(options))
            {
                Assert.Equal(2, assertContext.VideoCategory.First().CategoryId);
                Assert.Equal(1, assertContext.VideoCategory.Count());
            }
        }

        [Fact]
        public async Task CreatesNavigationProperty_VideoCategory()
        {
            var options = DbContextMock.GetOptions("CreatesNavigationProperty_VideoCategory");
            var mockFile = new Mock<IFormFile>();
            var mockFileCommands = new Mock<IFileCommands>();
            var mockCategoryQueries = new Mock<ICategoryQueries>();
            var categories = new List<string>();

            using (var context = new BubaTubeDbContext(options))
            {
                context.Category.AddRange(CategoryMockData.GetListOfCategoryModels());
                context.SaveChanges();

                var categoryFromDb = context.Category
                     .First(x => x.IsАpproved == true);
                mockCategoryQueries
                   .Setup(mock => mock.TakeAllCategoryIds(categories))
                   .Returns(new List<int>() { categoryFromDb.Id });

                var model = this.GetVideoDto();
                model.Categories = categories;

                var uploadVideoService = new VideoCommands(
                    context, mockFileCommands.Object, mockCategoryQueries.Object, fakeMapper);

                await uploadVideoService.Save(model, mockFile.Object);

                Assert.Equal(categoryFromDb.CategoryName,
                    context.VideoCategory.First().Category.CategoryName);
            }
        }

        [Fact]
        public async Task DeleteVideo()
        {
            var options = DbContextMock.GetOptions("DeleteVideo");
            var mockFile = new Mock<IFormFile>();
            var mockFileCommands = new Mock<IFileCommands>();
            var mockCategoryQueries = new Mock<ICategoryQueries>();

            mockFileCommands
                .Setup(x => x.Delete("test"))
                .Returns(true);

            using (var context = new BubaTubeDbContext(options))
            {
                context.Videos.Add(new Video
                {
                    Title = "test",
                    Path = "test"
                });
                context.SaveChanges();

                int videoId = context.Videos.First().Id;

                var uploadService = new VideoCommands(
                    context, mockFileCommands.Object, mockCategoryQueries.Object, fakeMapper);
                bool isSuccess = await uploadService.Delete(videoId);

                Assert.True(isSuccess);
            }

            using (var context = new BubaTubeDbContext(options))
            {
                var isDeleted = context.Videos.First().IsDeleted;

                Assert.True(isDeleted);
            }
        }

        [Fact]
        public async Task DeleteVideo_FileDoesNotExist_OnFileSystem()
        {
            var options = DbContextMock.GetOptions("DeleteVideo_FileDoesNotExist_OnFileSystem");
            var mockFile = new Mock<IFormFile>();
            var mockFileCommands = new Mock<IFileCommands>();
            var mockCategoryQueries = new Mock<ICategoryQueries>();

            mockFileCommands
                .Setup(x => x.Delete("test"))
                .Returns(false);

            using (var context = new BubaTubeDbContext(options))
            {
                context.Videos.Add(new Video
                {
                    Title = "test",
                    Path = "test"
                });
                context.SaveChanges();

                int videoId = context.Videos.First().Id;

                var uploadService = new VideoCommands(
                    context, mockFileCommands.Object, mockCategoryQueries.Object, fakeMapper);
                bool isSuccess = await uploadService.Delete(videoId);

                Assert.False(isSuccess);
            }
        }

        [Fact]
        public async Task DeleteVideo_FileDoesNotExist_InDatabase()
        {
            var options = DbContextMock.GetOptions("DeleteVideo_FileDoesNotExist_InDatabase");
            var mockFile = new Mock<IFormFile>();
            var mockFileCommands = new Mock<IFileCommands>();
            var mockCategoryQueries = new Mock<ICategoryQueries>();

            mockFileCommands
                .Setup(x => x.Delete("test"))
                .Returns(true);

            using (var context = new BubaTubeDbContext(options))
            {
                var uploadService = new VideoCommands(
                    context, mockFileCommands.Object, mockCategoryQueries.Object, fakeMapper);
                bool isSuccess = await uploadService.Delete(1);

                Assert.False(isSuccess);
            }
        }

        [Fact]
        public async Task ApprovesVideo()
        {
            var options = DbContextMock.GetOptions("ApprovesVideo");
            var mockFile = new Mock<IFormFile>();
            var mockFileCommands = new Mock<IFileCommands>();
            var mockCategoryQueries = new Mock<ICategoryQueries>();

            using (var actContext = new BubaTubeDbContext(options))
            {
                actContext.Videos.Add(new Video
                {
                    Title = "test",
                    Path = "test"
                });
                actContext.SaveChanges();

                int videoId = actContext.Videos.First().Id;

                var uploadService = new VideoCommands(
                    actContext, mockFileCommands.Object, mockCategoryQueries.Object, fakeMapper);
                bool isSuccess = await uploadService.Approve(videoId);

                Assert.True(isSuccess);
            }

            using (var assertContext = new BubaTubeDbContext(options))
            {
                Video video = assertContext.Videos
                    .First();

                Assert.True(video.IsАpproved);
            }
        }

        private VideoDTO GetVideoDto()
        {
            return new VideoDTO
            {
                Title = "testVideo",
                AuthorUserId = "1234",
                Description = "test",
                Likes = 1,
                Path = @"\Folder\Name.mp4",
                Url = "Name.mp4",
                Categories = new List<string>()
            };
        }
    }
}
