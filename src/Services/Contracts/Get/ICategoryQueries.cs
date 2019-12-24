using System.Collections.Generic;

namespace Services.Contracts.Get
{
    public interface ICategoryQueries
    {
        IEnumerable<int> TakeCategoryIds(IEnumerable<string> categories);
    }
}
