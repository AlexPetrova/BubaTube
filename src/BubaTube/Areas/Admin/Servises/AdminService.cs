using System.Collections.Generic;
using System.Linq;
using BubaTube.Areas.Admin.Servises.Contracts;

namespace BubaTube.Areas.Admin.Servises
{
    public class AdminService : IAdminService
    {
        private readonly BubaTubeDbContext context;

        public AdminService(BubaTubeDbContext context)
        {
            this.context = context;
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
