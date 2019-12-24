using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BubaTube.Helpers.Constants
{
    public interface IFileStreamHelper
    {
        Task SaveFile(IFormFile video, string path);
    }
}
