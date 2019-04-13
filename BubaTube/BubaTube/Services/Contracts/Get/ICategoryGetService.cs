using System.Collections.Generic;

namespace BubaTube.Services.Contracts.Get
{
    public interface ICategoryGetService
    {
        IEnumerable<int> TakeCategoryIds(IEnumerable<string> categories);
    }
}
