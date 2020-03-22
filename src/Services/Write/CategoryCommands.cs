using Contracts.Data.Models;
using DataAccess;
using Services.Contracts.Write;
using System.Collections.Generic;
using System.Linq;

namespace Services.Write
{
    public class CategoryCommands : ICategoryCommands
    {
        private readonly BubaTubeDbContext context;

        public CategoryCommands(BubaTubeDbContext context)
        {
            this.context = context;
        }

        public void SaveToDatabase(IEnumerable<string> categories)
        {
            this.context.Category.AddRange(categories
                .Select(x => new Category()
                {
                    CategoryName = x,
                    VideoCategory = new List<VideoCategory>()
                }));

            this.context.SaveChanges();
        }
    }
}
