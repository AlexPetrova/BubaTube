using BubaTube.Data.DTO;
using Microsoft.AspNetCore.Http;

namespace BubaTube.Services.Contracts
{
    public interface IUploadVideoService
    {
        void SaveToDatabase(VideoDTO video);

        void SaveVideoToRootFolder(IFormFile video, string path);
    }
}
