using BubaTube.Areas.Admin.Servises.Contracts;
using BubaTube.Helpers.Map;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Areas.Admin.Servises
{
    public class SearchUsetsService : ISearchUsersService
    {
        private readonly UserManager<User> userManager;
        private readonly BubaTubeDbContext context;

        public SearchUsetsService(
            UserManager<User> userManager,
            BubaTubeDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<UserDTO> ByEmail(string email)
        {
            var result = await this.userManager.FindByEmailAsync(email);

            if (result == null)
            {
                return new UserDTO();
            }

            return new UserDTO()
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                LastLogin = result.LastLogin,
                RegisteredOn = result.RegisteredOn,
                AvatarImage = result.AvatarImage
            };
        }

        public IEnumerable<UserDTO> ByLastActivity(int months)
        {
            var startingTime = DateTime.Now.AddMonths(months * -1);
            var result = this.context.Users
                .Where(x => x.LastLogin >= startingTime)
                .Select(Map.User())
                .ToList();

            return result;
        }

        public IEnumerable<UserDTO> ByNames(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> ByUserId(string userId)
        {
            var result = await this.userManager.FindByIdAsync(userId);

            if (result == null)
            {
                return new UserDTO();
            }

            return new UserDTO()
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                LastLogin = result.LastLogin,
                RegisteredOn = result.RegisteredOn,
                AvatarImage = result.AvatarImage
            };
        }

        public IEnumerable<UserDTO> RegisterdInPeriod(DateTime fromDate, DateTime toDate)
        {
            return this.context.Users
                .Where(x => x.RegisteredOn > fromDate && x.RegisteredOn < toDate)
                .Select(Map.User())
                .ToList();
        }

        public IEnumerable<UserDTO> RegisteredAfter(DateTime date)
        {
            return this.context.Users
                   .Where(x => x.RegisteredOn > date)
                   .Select(Map.User())
                   .ToList();
        }
    }
}
