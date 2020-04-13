using Microsoft.AspNetCore.Http;
using Services.Contracts.Write;
using Services.Wrappers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Services.Write
{
    public class FileCommands : IFileCommands
    {
        private readonly Func<string, FileMode, FileStream> createFileStream;
        private readonly FileWrapper fileWrapper;

        public FileCommands(
            Func<string, FileMode, FileStream> createFileStream,
            FileWrapper fileWrapper)
        {
            this.createFileStream = createFileStream;
            this.fileWrapper = fileWrapper;
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
            if(this.fileWrapper.Exists(path))
            {
                this.fileWrapper.Delete(path);

                return true;
            }

            return false;
        }
    }
}
