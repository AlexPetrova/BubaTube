using Contracts.Data.Models;

namespace Services.Contracts.Write
{
    public interface IApproveCommands
    {
        Video ApproveVideo(int id);

        Category ApproveCategory(int id);
    }
}
