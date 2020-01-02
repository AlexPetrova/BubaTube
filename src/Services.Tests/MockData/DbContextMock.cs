using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Services.Tests.MockData
{
    public static class DbContextMock
    {
        public static DbContextOptions<BubaTubeDbContext> GetOptions(string name)
        {
            return new DbContextOptionsBuilder<BubaTubeDbContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;
        }
    }
}
