using BubaTube_Tests.MockData;
using Contracts.Data.DTO;
using Contracts.Data.Models;
using DataAccess;
using Services.Get;
using Services.Tests.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Services.Tests.Get
{
    public class CategoryQueries_Should
    {
        static readonly Func<Category, CategoryDTO> fakeMapper = _ => new CategoryDTO();

        [Fact]
        public void ReturnsListOfIds_WhenCategories_Present()
        {
            var options = DbContextMock.GetOptions("ReturnsListOfIds_WhenCategories_Present");
            var searchedCategories = new List<string>() { "Test1", "Test2", "TestTest" };

            using (var context = new BubaTubeDbContext(options))
            {
                context.Category.AddRange(CategoryMockData.GetListOfCategoryModels());
                context.SaveChanges();

                var categoryGetService = new CategoryQueries(context, fakeMapper);
                var result = categoryGetService.TakeApprovedCategoryIds(searchedCategories);

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
        public void ReturnsEmptyList_WhenCategories_NotPresent()
        {
            var options = DbContextMock.GetOptions("ReturnsEmptyList_WhenCategories_NotPresent");
            var searchedCategories = new List<string>() { "TestTest1", "TestTest2", "TestTest" };

            using (var context = new BubaTubeDbContext(options))
            {
                context.Category.AddRange(CategoryMockData.GetListOfCategoryModels());
                context.SaveChanges();

                var categoryGetService = new CategoryQueries(context, fakeMapper);
                var result = categoryGetService.TakeApprovedCategoryIds(searchedCategories);

                Assert.Empty(result);
            }
        }

        [Fact]
        public void ReturnsEmptyList_WhenCategory_NotApproved()
        {
            var options = DbContextMock.GetOptions("ReturnsEmptyList_WhenCategory_NotApproved");
            var searchedCategories = new List<string>() { "Test0" };

            using (var context = new BubaTubeDbContext(options))
            {
                context.Category.AddRange(CategoryMockData.GetListOfCategoryModels());
                context.SaveChanges();

                var categoryGetService = new CategoryQueries(context, fakeMapper);
                var result = categoryGetService.TakeApprovedCategoryIds(searchedCategories);

                Assert.Empty(result);
            }
        }

        [Fact]
        public void ReturnsAllCategories()
        {
            var options = DbContextMock.GetOptions("ReturnsAllCategories");
            var searchedCategories = new List<string>() { "Test0" };
            IList<int> result;

            using (var context = new BubaTubeDbContext(options))
            {
                context.Category.AddRange(CategoryMockData.GetListOfCategoryModels());
                context.SaveChanges();

                var categoryGetService = new CategoryQueries(context, fakeMapper);
                result = categoryGetService.TakeAllCategoryIds(searchedCategories);

                Assert.Equal(1, result.Count);
            }
        }
    }
}
