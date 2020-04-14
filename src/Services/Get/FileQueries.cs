using Services.Contracts;
using Services.Contracts.Get;
using Services.Extensions;
using System;
using System.IO;

namespace Services.Get
{
    public class FileQueries : IFileQueries
    {
        // todo take from config root and subfolder
        private const string rootFolder = "wwwroot";
        private const string videoSubFolder = "video";

        public PathInfo GenerateVideoPath(string fileExtension)
        {
            var folder = Path.Combine(rootFolder, videoSubFolder);
            var fileName = new Random().ShortString() + fileExtension;
            var path = Path.Combine(folder, fileName.ToString());

            return new PathInfo
            {
                Path = path,
                FileName = fileName
            };
        }

        public string GetVideoPath(string fileName)
        {
            var folder = Path.Combine(rootFolder, videoSubFolder);

            return Path.Combine(folder, fileName);
        }
    }
}
