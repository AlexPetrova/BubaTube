using Contracts.Data.Models;
using DataAccess;
using Services.Contracts.Write;
using System.Linq;

namespace Services.Write
{
    public class ApproveCommands : IApproveCommands
    {
        private readonly BubaTubeDbContext context;

        public ApproveCommands(BubaTubeDbContext context)
        {
            this.context = context;
        }

        public Category ApproveCategory(int id)
        {
            var category = this.context.Categories
                .First(x => x.Id == id);

            category.IsАpproved = true;

            return this.context.SaveChanges() == 1 ? category : new Category();
        }

        public Video ApproveVideo(int id)
        {
            var video = this.context.Videos
                .First(x => x.Id == id);

            video.IsАpproved = true;

            return this.context.SaveChanges() == 1 ? video : new Video();
        }
    }
}
