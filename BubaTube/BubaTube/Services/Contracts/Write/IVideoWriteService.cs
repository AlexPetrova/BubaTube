using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BubaTube.Services.Contracts.Write
{
    public interface IVideoWriteService
    {
        Video Save(VideoDTO video);

        Task Save(IFormFile video, string path);
    }
}
