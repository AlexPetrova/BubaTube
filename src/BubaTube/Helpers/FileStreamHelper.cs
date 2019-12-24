using BubaTube.Factory.Contracts;
using BubaTube.Helpers.Constants;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace BubaTube.Helpers
{
    public class FileStreamHelper : IFileStreamHelper
    {
        private readonly IFileStreamFactory fileStreamFactory;

        public FileStreamHelper(IFileStreamFactory fileStreamFactory)
        {
            this.fileStreamFactory = fileStreamFactory;
        }

        public async Task SaveFile(IFormFile video, string path)
        {
            //TODO handle exception and log it
            using (var fileStream = this.fileStreamFactory.CreateFileStreamInstance(
                path, FileMode.Create))
            {
                await video.CopyToAsync(fileStream);
            }
        }
    }
}
