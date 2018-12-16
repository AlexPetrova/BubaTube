using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using BubaTube.Factory.Contracts;
using BubaTube.Services.Contracts.Get;
using BubaTube.Services.WriteServices;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BubaTube_Tests.Services.WriteServices
{
    public class VideoGetServiceShould
    {
        [Fact]
        public void SaveToDatabase()
        {
            var options = this.GetOptions("SaveVideo");
            var fileStreamFactory = new Mock<IFileStreamFactory>();
            var categoryGetService = new Mock<ICategoryGetService>();

            using (var context = new BubaTubeDbContext(options))
            {
                var uploadService = new VideoWriteService(
                    context, 
                    fileStreamFactory.Object,
                    categoryGetService.Object);

                var model = this.GetVideoDto();

                var video = uploadService.SaveToDatabase(model);

                Assert.Equal(1, context.Videos.Count());
                Assert.True(context.Videos.Any(x => x.Id == video.Id));
            }
        }

        [Fact]
        public void SaveToDatabaseCorrectData()
        {
            var options = this.GetOptions("SaveToDatabaseCorrectData");
            var fileStreamFactory = new Mock<IFileStreamFactory>();
            var categoryGetService = new Mock<ICategoryGetService>();

            using (var context = new BubaTubeDbContext(options))
            {
                var uploadVideoService = new VideoWriteService(
                    context, 
                    fileStreamFactory.Object,
                    categoryGetService.Object);

                var model = this.GetVideoDto();

                uploadVideoService.SaveToDatabase(model);
                var savedModelInDb = context.Videos.First();

                Assert.Equal(model.Title, savedModelInDb.Title);
                Assert.Equal(model.AuthorId, savedModelInDb.AuthorId);
                Assert.Equal(model.Description, savedModelInDb.Description);
                Assert.Equal(model.Path, savedModelInDb.Path);
            }
        }

        [Fact]
        public void SaveToDatabaseAddsCorrectlyListOfCategoriesPerModel()
        {
            var options = this.GetOptions("SaveToDatabaseAddsCorrectlyListOfCategoriesPerModel");
            var fileStreamFactory = new Mock<IFileStreamFactory>();
            var categoryGetService = new Mock<ICategoryGetService>();
            var categories = new List<string>() { "Test1" };

            categoryGetService
                .Setup(mock => mock.TakeCategoryIds(categories))
                .Returns(new List<int>() { 2 });

            using (var context = new BubaTubeDbContext(options))
            {
                var model = this.GetVideoDto();
                model.Categories = categories;

                var uploadVideoService = new VideoWriteService(
                    context, 
                    fileStreamFactory.Object,
                    categoryGetService.Object);

                var savedVideo = uploadVideoService.SaveToDatabase(model);

                Assert.Equal(1, savedVideo.VideoCategory.Count);
                Assert.Equal(2, savedVideo.VideoCategory.First().CategoryId);
            }
        }

        [Fact]
        public void SaveToDatabaseCreatesNavigationPropertyBetweenVideoAndCategory()
        {
            var options = this.GetOptions("SaveToDatabaseAddsCorrectlyListOfCategoriesPerModel");
            var fileStreamFactory = new Mock<IFileStreamFactory>();
            var categoryGetService = new Mock<ICategoryGetService>();
            var categories = new List<string>() { "Test1" };

            categoryGetService
                .Setup(mock => mock.TakeCategoryIds(categories))
                .Returns(new List<int>() { 2 });

            using (var context = new BubaTubeDbContext(options))
            {
                context.Category.AddRange(this.GetListOfCategoryModels());
                context.SaveChanges();
                var model = this.GetVideoDto();
                model.Categories = categories;

                var uploadVideoService = new VideoWriteService(
                    context,
                    fileStreamFactory.Object,
                    categoryGetService.Object);

                var savedVideo = uploadVideoService.SaveToDatabase(model);
                
                Assert.Equal("Test1", savedVideo.VideoCategory.First().Category.CategoryName);
                Assert.Same(savedVideo, savedVideo.VideoCategory.First().Video);
            }
        }
        private DbContextOptions<BubaTubeDbContext> GetOptions(string name)
        {
            return new DbContextOptionsBuilder<BubaTubeDbContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;
        }

        private VideoDTO GetVideoDto()
        {
            return new VideoDTO
            {
                Title = "testVideo",
                AuthorId = "1234",
                Description = "test",
                Likes = 1,
                Path = @"\Folder\Name.mp4"
            };
        }

        private IEnumerable<Category> GetListOfCategoryModels()
        {
            return new List<Category>()
            {
                new Category()
                {
                    CategoryName = "Test0",
                    IsАpproved = false
                },
                new Category()
                {
                    CategoryName = "Test1",
                    IsАpproved = true
                },
                new Category()
                {
                    CategoryName = "Test2",
                    IsАpproved = true
                },
                new Category()
                {
                    CategoryName = "Test3",
                    IsАpproved = true
                },
                new Category()
                {
                    CategoryName = "Test4",
                    IsАpproved = false
                },
            };
        }
    }
}
