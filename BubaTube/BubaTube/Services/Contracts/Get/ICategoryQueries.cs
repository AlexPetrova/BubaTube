using System.Collections.Generic;

namespace BubaTube.Services.Contracts.Get
{
    public interface ICategoryQueries
    {
        IEnumerable<int> TakeCategoryIds(IEnumerable<string> categories);
    }
}
