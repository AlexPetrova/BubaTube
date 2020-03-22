using Contracts.Data.Models;
using System.Collections.Generic;

namespace Services.Contracts.Write
{
    public interface ICategoryCommands
    {
        void SaveToDatabase(IEnumerable<string> categories);
    }
}
