using Contracts.Data.Models;
using DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Contracts.Write;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Write
{
    public class UserCommands : IUserCommands
    {
        private readonly BubaTubeDbContext context;
        private readonly UserManager<User> userManager;

        public UserCommands(
            BubaTubeDbContext context,
            UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<int> SaveLoginDate(string userID)
        {
            this.context.Users
                .FirstOrDefault(x => x.Id == userID)
                .LastLogin = DateTime.Now;
            var result = await context.SaveChangesAsync();

            return result;
        }

        public async Task<int> CloseAccount(string userEmail)
        {
            var user = await this.userManager.FindByEmailAsync(userEmail);
            this.context.Entry(user).State = EntityState.Deleted;
            return await this.context.SaveChangesAsync();
        }
    }
}
