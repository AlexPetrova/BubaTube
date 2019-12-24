using Microsoft.AspNetCore.Http;
using Services.Contracts.Write;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BubaTube.Helpers
{
    public class FileCommands : IFileCommands
    {
        // TODO register this Func - no need for factory
        private readonly Func<string, FileMode, FileStream> fileStreamInstanceCreator = (name, mode) => { return new FileStream(name, mode); };

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
