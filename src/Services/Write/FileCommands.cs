using Microsoft.AspNetCore.Http;
using Services.Contracts.Write;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Services.Write
{
    public class FileCommands : IFileCommands
    {
        private readonly Func<string, FileMode, FileStream> createFileStream;

        public FileCommands(Func<string, FileMode, FileStream> createFileStream)
        {
            this.createFileStream = createFileStream;
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
            if(File.Exists(path))
            {
                File.Delete(path);

                return true;
            }

            return false;
        }
    }
}
