using BubaTube.Data;
using BubaTube.Services.GetServices;
using BubaTube_Tests.MockData;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BubaTube_Tests.Services.GetServices.CategoryGetServiceTests
{
    public class CategoryGetService_TakeCategoryIds
    {
        [Fact]
        public void ReturnsListOfIdsWhenPassedCategoriesArePresentInDB()
        {
            var options = this.GetOptions("TakeCategoryIdsTest");
            var searchedCategories = new List<string>() { "Test1", "Test2", "TestTest" };

            using (var context = new BubaTubeDbContext(options))
            {
                context.Category.AddRange(CategoryMockData.GetListOfCategoryModels());
                context.SaveChanges();

                var categoryGetService = new CategoryGetService(context);
                var result = categoryGetService.TakeCategoryIds(searchedCategories);

                var test1FromDb = context.Category
                    .FirstOrDefault(x => x.CategoryName == "Test1");

                var test2FromDb = context.Category
                    .FirstOrDefault(x => x.CategoryName == "Test2");
                
                Assert.NotEmpty(result);
                Assert.Equal(2, result.Count());
                Assert.Contains(result, x => x == test1FromDb.Id);
                Assert.Contains(result, x => x == test2FromDb.Id);
            }
        }

        [Fact]
        public void ReturnsEmptyListWhenPassedSCategoriesAreNotPresentInDB()
        {
            var options = this.GetOptions("TakeCategoryIdsTest");
            var searchedCategories = new List<string>() { "TestTest1", "TestTest2", "TestTest" };

            using (var context = new BubaTubeDbContext(options))
            {
                context.Category.AddRange(CategoryMockData.GetListOfCategoryModels());
                context.SaveChanges();

                var categoryGetService = new CategoryGetService(context);
                var result = categoryGetService.TakeCategoryIds(searchedCategories);
                
                Assert.Empty(result);
            }
        }

        [Fact]
        public void ReturnsEmptyListWhenPassedCategoryIsNotApproved()
        {
            var options = this.GetOptions("TakeCategoryIdsTest");
            var searchedCategories = new List<string>() { "Test0" };

            using (var context = new BubaTubeDbContext(options))
            {
                context.Category.AddRange(CategoryMockData.GetListOfCategoryModels());
                context.SaveChanges();

                var categoryGetService = new CategoryGetService(context);
                var result = categoryGetService.TakeCategoryIds(searchedCategories);

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
