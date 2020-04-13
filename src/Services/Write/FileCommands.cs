using Microsoft.AspNetCore.Http;
using Services.Contracts.Wrappers;
using Services.Contracts.Write;
using Services.Wrappers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Services.Write
{
    public class FileCommands : IFileCommands
    {
        private readonly Func<string, FileMode, Stream> createFileStream;
        private readonly IFile fileProvider;

        public FileCommands(
            Func<string, FileMode, Stream> createFileStream,
            IFile fileProvider)
        {
            this.createFileStream = createFileStream;
            this.fileProvider = fileProvider;
        }

        public async Task Save(IFormFile video, string path)
        {
            //TODO handle exception and log it
            using (var fileStream = this.createFileStream(path, FileMode.Create))
            {
                await video.CopyToAsync(fileStream);
            }
        }

        public bool Delete(string path)
        {
            if(this.fileProvider.Exists(path))
            {
                this.fileProvider.Delete(path);

                return true;
            }

            return false;
        }
    }
}
