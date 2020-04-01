using Contracts.Data.DTO;
using Contracts.Data.Models;
using DataAccess;
using Services.Contracts.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Get
{
    public class UserQueries : IUserQueries
    {
        private readonly BubaTubeDbContext context;
        private readonly Expression<Func<User, UserDTO>> userMapper;

        public UserQueries(
            BubaTubeDbContext context,
            Expression<Func<User, UserDTO>> userMapper)
        {
            this.context = context;
            this.userMapper = userMapper;
        }

        public IReadOnlyCollection<UserDTO> RegisterdInPeriod(DateTime fromDate, DateTime toDate)
        {
            return this.context.Users
                .Where(x => x.RegisteredOn <= fromDate && x.RegisteredOn >= toDate)
                .Select(this.userMapper)
                .ToList();
        }

        public IReadOnlyCollection<UserDTO> ByLastActivity(int months)
        {
            var startingTime = DateTime.Now.AddMonths(months * -1);
            return this.context.Users
                .Where(x => x.LastLogin >= startingTime)
                .Select(this.userMapper)
                .ToList();
        }
    }
}
