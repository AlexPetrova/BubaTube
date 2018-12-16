using BubaTube.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BubaTube_Tests.MockData
{
    internal class CategoryMockData
    {
        internal IEnumerable<Category> GetListOfCategoryModels()
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
