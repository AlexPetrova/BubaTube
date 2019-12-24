using Microsoft.AspNetCore.Http;
using Services.Contracts.Write;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Services.Write
{
    public class FileCommands : IFileCommands
    {
        private readonly Func<string, FileMode, FileStream> fileStreamInstanceCreator;

        public FileCommands(Func<string, FileMode, FileStream> fileStreamInstanceCreator)
        {
            this.fileStreamInstanceCreator = fileStreamInstanceCreator;
        }

        public async Task Save(IFormFile video, string path)
        {
            //TODO handle exception and log it
            using (var fileStream = this.fileStreamInstanceCreator(path, FileMode.Create))
            {
                await video.CopyToAsync(fileStream);
            }
        }
    }
}
