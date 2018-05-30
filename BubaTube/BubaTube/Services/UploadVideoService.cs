using BubaTube.Data;
using BubaTube.Services.Contracts;

namespace BubaTube.Services
{
    public class UploadVideoService : IUploadVideoService
    {
        private BubaTubeDbContext context;

        public UploadVideoService(BubaTubeDbContext context)
        {
            this.context = context;
        }
    }
}
