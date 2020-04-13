using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Services.Contracts.Write
{
    public interface IFileCommands
    {
        Task Save(IFormFile video, string path);

        bool Delete(string path);
    }
}
