using BubaTube.Data;
using BubaTube.Services.Contracts.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Services.GetServices
{
    public class CategoryGetService : ICategoryGetService
    {
        private BubaTubeDbContext context;

        public CategoryGetService(BubaTubeDbContext context)
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
    }
}
