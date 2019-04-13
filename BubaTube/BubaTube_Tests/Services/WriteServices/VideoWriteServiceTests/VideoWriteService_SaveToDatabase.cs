using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Factory.Contracts;
using BubaTube.Services.Contracts.Get;
using BubaTube.Services.WriteServices;
using BubaTube_Tests.MockData;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BubaTube_Tests.Services.WriteServices.VideoWriteServiceTest
{
    public class VideoWriteService_SaveToDatabase
    {
        [Fact]
        public void SavesPassedData()
        {
            var options = DbContextMock.GetOptions("SaveToDatabaseTest");
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
                var savedModelInDb = context.Videos.First();

                Assert.Equal(1, context.Videos.Count());
                Assert.True(context.Videos.Any(x => x.Id == video.Id));
                Assert.Equal(model.Title, savedModelInDb.Title);
                Assert.Equal(model.AuthorId, savedModelInDb.AuthorId);
                Assert.Equal(model.Description, savedModelInDb.Description);
                Assert.Equal(model.Path, savedModelInDb.Path);
            }
        }

        [Fact]
        public void AddsCorrectlyListOfCategoriesPerModel()
        {
            var options = DbContextMock.GetOptions("SaveToDatabaseTest");
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
        public void CreatesNavigationPropertyBetweenVideoAndCategory()
        {
            var options = DbContextMock.GetOptions("SaveToDatabaseTest");
            var fileStreamFactory = new Mock<IFileStreamFactory>();
            var categoryGetService = new Mock<ICategoryGetService>();
            var categories = new List<string>();

            using (var context = new BubaTubeDbContext(options))
            {
                context.Category.AddRange(CategoryMockData.GetListOfCategoryModels());
                context.SaveChanges();

                var categoryFromDb = context.Category
                     .First(x => x.IsАpproved == true);
                categoryGetService
                   .Setup(mock => mock.TakeCategoryIds(categories))
                   .Returns(new List<int>() { categoryFromDb.Id });

                var model = this.GetVideoDto();
                model.Categories = categories;

                var uploadVideoService = new VideoWriteService(
                    context,
                    fileStreamFactory.Object,
                    categoryGetService.Object);

                var savedVideo = uploadVideoService.SaveToDatabase(model);
                
                Assert.Equal(categoryFromDb.CategoryName, savedVideo.VideoCategory.First().Category.CategoryName);
                Assert.Same(savedVideo, savedVideo.VideoCategory.First().Video);
            }
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
    }
}