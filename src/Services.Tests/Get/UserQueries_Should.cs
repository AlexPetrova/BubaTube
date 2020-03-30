using Contracts.Data.DTO;
using Contracts.Data.Models;
using DataAccess;
using Services.Get;
using Services.Tests.MockData;
using System;
using System.Collections.Generic;
using Xunit;

namespace Services.Tests.Get
{
    public class UserQueries_Should
    {
        static readonly Func<User, UserDTO> fakeMapper = new Func<User, UserDTO>(_ => new UserDTO());

        [Fact]
        public void ReturnsUsers_LastActivity()
        {
            var options = DbContextMock.GetOptions("ReturnsUsers_LastActivity");
            IReadOnlyCollection<UserDTO> lastOneMonth;
            IReadOnlyCollection<UserDTO> lastThreeMonth;
            IReadOnlyCollection<UserDTO> lastFourMonth;

            using (var context = new BubaTubeDbContext(options))
            {
                context.AddRange(UserMockData.GetListOfUserModels());
                context.SaveChanges();

                var userQueries = new UserQueries(context, fakeMapper);

                lastOneMonth = userQueries.ByLastActivity(1);
                lastThreeMonth = userQueries.ByLastActivity(3);
                lastFourMonth = userQueries.ByLastActivity(4);
            }

            Assert.Equal(1, lastOneMonth.Count);
            Assert.Equal(3, lastThreeMonth.Count);
            Assert.Equal(6, lastFourMonth.Count);
        }
    }
}
