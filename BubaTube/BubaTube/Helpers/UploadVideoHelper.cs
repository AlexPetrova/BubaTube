using BubaTube.Helpers.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
