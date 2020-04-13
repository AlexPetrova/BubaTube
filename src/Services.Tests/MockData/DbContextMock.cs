using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Tests.MockData
{
    public static class DbContextMock
    {
        public static DbContextOptions<BubaTubeDbContext> GetOptions(string name)
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            return new DbContextOptionsBuilder<BubaTubeDbContext>()
                .UseInMemoryDatabase(databaseName: name)
                .UseInternalServiceProvider(serviceProvider)
                .Options;
        }
    }
}
