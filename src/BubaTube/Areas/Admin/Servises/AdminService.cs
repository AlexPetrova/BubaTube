using System.Collections.Generic;
using System.Linq;
using BubaTube.Areas.Admin.Servises.Contracts;
using BubaTube.Data;
using BubaTube.Data.DTO;

namespace BubaTube.Areas.Admin.Servises
{
    public class AdminService : IAdminService
    {
        private readonly BubaTubeDbContext context;

        public AdminService(BubaTubeDbContext context)
        {
            this.context = context;
        }

        public void ApproveCategory(int id)
        {
            var category = this.context.Category
                .First(x => x.Id == id);

            category.IsАpproved = true;

            this.context.SaveChanges();
        }

        public void ApproveVideo(int id)
        {
            var video = this.context.Videos
                .First(x => x.Id == id);

            video.IsАpproved = true;

            this.context.SaveChanges();
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

        public IEnumerable<VideoDTO> GetAllVideosForApproval()
        {
            var videosForApproval = this.context.Videos
                .Where(x => x.IsАpproved == false)
                .Select(x => new VideoDTO()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Likes = x.Likes,
                    AuthorUserName = x.Author.UserName,
                    Path = x.Path
                })
                .ToList();

            return videosForApproval;
        }
    }
}
