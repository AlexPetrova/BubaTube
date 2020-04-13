using Contracts.Data.DTO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Services.Contracts.Write
{
    public interface IVideoCommands
    {
        Task<int> Save(VideoDTO dto, IFormFile video);
        
        Task<bool> Delete(int id);

        Task<bool> Approve(int id);
    }
}
