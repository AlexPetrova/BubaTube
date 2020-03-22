using Contracts.Data.DTO;
using System.Collections.Generic;

namespace Services.Contracts.Get
{
    public interface ICategoryQueries
    {
        IList<int> TakeApprovedCategoryIds(IEnumerable<string> categories);

        IList<int> TakeAllCategoryIds(IEnumerable<string> categories);

        IReadOnlyCollection<CategoryDTO> GetAllForApproval();
    }
}
