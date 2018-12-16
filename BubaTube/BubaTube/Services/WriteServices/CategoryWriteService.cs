using BubaTube.Data;
using BubaTube.Data.Models;
using BubaTube.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BubaTube.Services.WriteServices
{
    public class CategoryWriteService : ICategoryWriteService
    {
        private BubaTubeDbContext context;

        public CategoryWriteService(BubaTubeDbContext context)
        {
            this.context = context;
        }

        public void SaveToDatabase(IEnumerable<string> categories)
        {
            var filteredCategories = this.FilterCategories(categories);

            this.context.Category.AddRange(filteredCategories);

            this.context.SaveChanges();
        }

        /// <summary>
        /// Takes as a parameter collection of string categories and returns the one that are not saved in the database, mapped to <see cref="Category"/> model.
        /// </summary>
        public IEnumerable<Category> FilterCategories(IEnumerable<string> categories)
        {
            var categoryNamesSavedInDB = this.context.Category
                .Select(x => x.CategoryName)
                .ToList();

            var different = categories.Except(categoryNamesSavedInDB);

            var filtered = different.
                Select(x => new Category()
                {
                    CategoryName = x,
                    VideoCategory = new List<VideoCategory>()
                })
                .ToList();

            return filtered;
        }
    }
}
