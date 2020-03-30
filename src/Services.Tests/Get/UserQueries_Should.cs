using Contracts.Data.DTO;
using Contracts.Data.Models;
using DataAccess;
using Services.Get;
using Services.Tests.MockData;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Services.Tests.Get
{
    public class UserQueries_Should
    {
        static readonly Expression<Func<User, UserDTO>> fakeMapper = u  => new UserDTO();

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

        [Fact]
        public void ReturnsUsers_RegisterAfter()
        {
            var options = DbContextMock.GetOptions("ReturnsUsers_RegisterAfter");
            IReadOnlyCollection<UserDTO> oneMonth;
            IReadOnlyCollection<UserDTO> threeMonth;
            IReadOnlyCollection<UserDTO> fourMonth;

            using (var context = new BubaTubeDbContext(options))
            {
                context.AddRange(UserMockData.GetListOfUserModels());
                context.SaveChanges();

                var userQueries = new UserQueries(context, fakeMapper);

                oneMonth = userQueries.RegisteredAfter(DateTime.Now.AddDays(-1));
                threeMonth = userQueries.RegisteredAfter(DateTime.Now.AddMonths(-3));
                fourMonth = userQueries.RegisteredAfter(DateTime.Now.AddMonths(-6));
            }

            Assert.Equal(0, oneMonth.Count);
            Assert.Equal(3, threeMonth.Count);
            Assert.Equal(6, fourMonth.Count);
        }


        [Fact]
        public void ReturnsUsers_RegisteredInPeriod()
        {
            var options = DbContextMock.GetOptions("ReturnsUsers_RegisteredInPeriod ");
            IReadOnlyCollection<UserDTO> oneMonth;
            IReadOnlyCollection<UserDTO> threeMonth;
            IReadOnlyCollection<UserDTO> fourMonth;

            using (var context = new BubaTubeDbContext(options))
            {
                context.AddRange(UserMockData.GetListOfUserModels());
                context.SaveChanges();

                var userQueries = new UserQueries(context, fakeMapper);

                oneMonth = userQueries.RegisterdInPeriod(DateTime.Now, DateTime.Now.AddDays(-1));
                threeMonth = userQueries.RegisterdInPeriod(DateTime.Now, DateTime.Now.AddMonths(-3));
                fourMonth = userQueries.RegisterdInPeriod(DateTime.Now, DateTime.Now.AddMonths(-6));
            }

            Assert.Equal(0, oneMonth.Count);
            Assert.Equal(3, threeMonth.Count);
            Assert.Equal(6, fourMonth.Count);
        }
    }
}
