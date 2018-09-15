using BubaTube.Data.DTO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BubaTube.Services.Contracts
{
    public interface IUploadVideoService
    {
        void SaveToDatabase(VideoDTO video);

        Task SaveVideoToRootFolder(IFormFile video, string path);
    }
}
