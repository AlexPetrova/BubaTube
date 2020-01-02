using Contracts.Data.DTO;
using System.Collections.Generic;

namespace Services.Contracts.Get
{
    public interface ICategoryQueries
    {
        IEnumerable<int> TakeCategoryIds(IEnumerable<string> categories);

        IReadOnlyCollection<CategoryDTO> GetAllForApproval();
    }
}
