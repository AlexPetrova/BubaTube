using BubaTube.Areas.Admin.Servises.Contracts;
using BubaTube.Data;
using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BubaTube.Areas.Admin.Servises
{
    public class SearchUsetsService : ISearchUsersService
    {
        private UserManager<User> userManager;
        private BubaTubeDbContext context;

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

            if(result == null)
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IEnumerable<UserDTO> RegisteredAfter(DateTime fromDate)
        {
            throw new NotImplementedException();
        }
    }
}
