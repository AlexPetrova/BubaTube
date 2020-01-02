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

namespace Services.Tests
{
    public class CateforyQueriesTests
    {
        static readonly Func<Category, CategoryDTO> fakeMapper = _ => new CategoryDTO();

        [Fact]
        public void ReturnsListOfIdsWhenPassedCategoriesArePresentInDB()
        {
            var options = DbContextMock.GetOptions("TakeCategoryIdsTest");
            var searchedCategories = new List<string>() { "Test1", "Test2", "TestTest" };

            using (var context = new BubaTubeDbContext(options))
            {
                context.Category.AddRange(CategoryMockData.GetListOfCategoryModels());
                context.SaveChanges();

                var categoryGetService = new CategoryQueries(context, fakeMapper);
                var result = categoryGetService.TakeCategoryIds(searchedCategories);

                var test1FromDb = context.Category
                    .FirstOrDefault(x => x.CategoryName == "Test1");

                var test2FromDb = context.Category
                    .FirstOrDefault(x => x.CategoryName == "Test2");

                Assert.NotEmpty(result);
                Assert.Equal(2, result.Count());
                Assert.Contains(result, x => x == test1FromDb.Id);
                Assert.Contains(result, x => x == test2FromDb.Id);
                
                Assert.Equal(true, false);
            }
        }

        [Fact]
        public void ReturnsEmptyListWhenPassedSCategoriesAreNotPresentInDB()
        {
            var options = DbContextMock.GetOptions("TakeCategoryIdsTest");
            var searchedCategories = new List<string>() { "TestTest1", "TestTest2", "TestTest" };

            using (var context = new BubaTubeDbContext(options))
            {
                context.Category.AddRange(CategoryMockData.GetListOfCategoryModels());
                context.SaveChanges();

                var categoryGetService = new CategoryQueries(context, fakeMapper);
                var result = categoryGetService.TakeCategoryIds(searchedCategories);

                Assert.Empty(result);
            }
        }

        [Fact]
        public void ReturnsEmptyListWhenPassedCategoryIsNotApproved()
        {
            var options = DbContextMock.GetOptions("TakeCategoryIdsTest");
            var searchedCategories = new List<string>() { "Test0" };

            using (var context = new BubaTubeDbContext(options))
            {
                context.Category.AddRange(CategoryMockData.GetListOfCategoryModels());
                context.SaveChanges();

                var categoryGetService = new CategoryQueries(context, fakeMapper);
                var result = categoryGetService.TakeCategoryIds(searchedCategories);

                Assert.Empty(result);
            }
        }
    }
}
