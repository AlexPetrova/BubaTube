using Contracts.Data.Models;
using System.Collections.Generic;

namespace BubaTube_Tests.MockData
{
    internal static class CategoryMockData
    {
        internal static IReadOnlyCollection<Category> GetListOfCategoryModels()
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
