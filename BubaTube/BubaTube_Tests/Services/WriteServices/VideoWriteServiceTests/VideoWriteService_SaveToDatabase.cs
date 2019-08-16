using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Factory.Contracts;
using BubaTube.Helpers.Constants;
using BubaTube.Services.Contracts.Get;
using BubaTube.Services.WriteServices;
using BubaTube_Tests.MockData;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BubaTube_Tests.Services.WriteServices.VideoWriteServiceTest
{
    public class VideoWriteService_SaveToDatabase
    {
        [Fact]
        public async Task SavesPassedData()
        {
            var options = DbContextMock.GetOptions("SaveToDatabaseTest");
            var fileStreamFactory = new Mock<IFileStreamHelper>();
            var categoryGetService = new Mock<ICategoryGetService>();
            var mockFile = new Mock<IFormFile>();

            using (var context = new BubaTubeDbContext(options))
            {
                var uploadService = new VideoWriteService(
                    context,
                    fileStreamFactory.Object,
                    categoryGetService.Object);

                var model = this.GetVideoDto();

                await uploadService.Save(model, mockFile.Object, "C:/Videos");
                var savedModelInDb = context.Videos.First();

                Assert.Equal(1, context.Videos.Count());
                Assert.Equal(model.Title, savedModelInDb.Title);
                Assert.Equal(model.AuthorUserName, savedModelInDb.AuthorId);
                Assert.Equal(model.Description, savedModelInDb.Description);
                Assert.Equal(model.Path, savedModelInDb.Path);
            }
        }

        [Fact]
        public async Task AddsCorrectlyListOfCategoriesPerModel()
        {
            var options = DbContextMock.GetOptions("SaveToDatabaseTest");
            var fileStreamFactory = new Mock<IFileStreamHelper>();
            var categoryGetService = new Mock<ICategoryGetService>();
            var mockFile = new Mock<IFormFile>();
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

                await uploadVideoService.Save(model, mockFile.Object, "C:/Videos" );

                Assert.Equal(2, context.VideoCategory.First().CategoryId);
                Assert.Equal(1, context.VideoCategory.Count());
            }
        }

        [Fact]
        public async Task CreatesNavigationPropertyBetweenVideoAndCategory()
        {
            var options = DbContextMock.GetOptions("SaveToDatabaseTest");
            var fileStreamFactory = new Mock<IFileStreamHelper>();
            var categoryGetService = new Mock<ICategoryGetService>();
            var mockFile = new Mock<IFormFile>();
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

                await uploadVideoService.Save(model, mockFile.Object, "C:/Videos");
                
                Assert.Equal(categoryFromDb.CategoryName, context.VideoCategory.First().Category.CategoryName);
            }
        }

        private VideoDTO GetVideoDto()
        {
            return new VideoDTO
            {
                Title = "testVideo",
                AuthorUserName = "1234",
                Description = "test",
                Likes = 1,
                Path = @"\Folder\Name.mp4"
            };
        }
    }
}