﻿using Contracts.Data.DTO;
using Contracts.Data.Models;
using DataAccess;
using Services.Contracts.Get;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Get
{
    public class CategoryQueries : ICategoryQueries
    {
        private readonly BubaTubeDbContext context;
        private readonly Func<Category, CategoryDTO> categoryMapper;

        public CategoryQueries(
            BubaTubeDbContext context,
            Func<Category, CategoryDTO> categoryMapper)
        {
            this.context = context;
            this.categoryMapper = categoryMapper;
        }

        public IList<int> TakeApprovedCategoryIds(IEnumerable<string> categories)
        {
            return this.context.Categories
                .Where(x => categories.Contains(x.CategoryName) && x.IsАpproved == true)
                .Select(x => x.Id)
                .ToList();
        }

        public IList<int> TakeAllCategoryIds(IEnumerable<string> categories)
        {
            return this.context.Categories
                .Where(x => categories.Contains(x.CategoryName))
                .Select(x => x.Id)
                .ToList();
        }

        public IReadOnlyCollection<CategoryDTO> GetAllForApproval()
        {
            return this.context.Categories
                .Where(x => x.IsАpproved == false)
                .Select(this.categoryMapper)
                .ToList();
        }
    }
}
