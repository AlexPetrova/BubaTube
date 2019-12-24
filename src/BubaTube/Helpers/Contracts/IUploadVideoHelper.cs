namespace BubaTube.Helpers.Contracts
{
    public interface IUploadVideoHelper
    {
        /// <summary>
        /// Generates path in wwwrooth folder using GUID for name of the video file. If not passed as a last parameter extension of the file, uses default - .mp4
        /// </summary>
        /// <returns>Path in wwwroot folder</returns>
        string GeneratePath(string environmetWebRootFolder, string fileExtension = null);
    }
}
