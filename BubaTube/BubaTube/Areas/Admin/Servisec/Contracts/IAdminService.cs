using BubaTube.Data.DTO;
using System.Collections.Generic;

namespace BubaTube.Areas.Admin.Servisec.Contracts
{
    public interface IAdminService
    {
        IEnumerable<VideoDTO> GetAllVideosForApproval();

        IEnumerable<CategoryDTO> GetAllCategoriesForApproval();

        void ApproveVideo(int id);

        void ApproveCategory(int id);
    }
}
