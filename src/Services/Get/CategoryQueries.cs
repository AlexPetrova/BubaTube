using Contracts.Data.DTO;
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

        public CategoryQueries(BubaTubeDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<int> TakeCategoryIds(IEnumerable<string> categories)
        {
            var result = this.context.Category
                .Where(x => categories.Contains(x.CategoryName) && x.IsАpproved == true)
                .Select(x => x.Id)
                .ToList();

            return result;
        }

        public IEnumerable<CategoryDTO> GetAllCategoriesForApproval()
        {
            var categoriesForApproval = this.context.Category
                .Where(x => x.IsАpproved == false)
                .Select(x => new CategoryDTO()
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName
                })
                .ToList();

            return categoriesForApproval;
        }
    }
}
