using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Helpers.Contracts
{
    public interface IUploadVideoHelper
    {
        /// <summary>
        /// Generates path in wwwrooth folder using GUID for name of the video file.
        /// </summary>
        /// <returns>Path in wwwroot folder</returns>
        string GeneratePath(string environmetWebRootFolder);
    }
}
