using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Services.Contracts.Get
{
    public interface ICategoryGetService
    {
        IEnumerable<int> TakeCategoryIds(IEnumerable<string> categories);
    }
}
