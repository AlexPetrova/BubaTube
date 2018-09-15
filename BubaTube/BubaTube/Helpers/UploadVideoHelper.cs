using BubaTube.Helpers.Contracts;
using System;
using System.IO;

namespace BubaTube.Helpers
{
    public class UploadVideoHelper : IUploadVideoHelper
    {
        public string GeneratePath(string environmetWebRootFolder)
        {
            var folder = Path.Combine(environmetWebRootFolder, "video");
            var nameOfVideo = Guid.NewGuid();
            var path = Path.Combine(folder, nameOfVideo.ToString() + ".mp4");

            return path;
        }
    }
}
