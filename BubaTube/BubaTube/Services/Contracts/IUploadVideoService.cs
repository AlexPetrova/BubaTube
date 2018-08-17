using BubaTube.Data.DTO;

namespace BubaTube.Services.Contracts
{
    public interface IUploadVideoService
    {
        void SaveToDatabase(VideoDTO video);
    }
}
