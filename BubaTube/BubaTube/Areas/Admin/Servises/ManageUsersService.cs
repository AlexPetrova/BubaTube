using BubaTube.Areas.Admin.Servises.Contracts;
using BubaTube.Data;
using BubaTube.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BubaTube.Areas.Admin.Servises
{
    public class ManageUsersService : IManageUsersService
    {
        private BubaTubeDbContext context;
        private UserManager<User> userManager;

        public ManageUsersService(
            BubaTubeDbContext context,
            UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<int> CloseAccount(string userEmail)
        {
            var user =await this.userManager.FindByEmailAsync(userEmail);
            this.context.Entry(user).State = EntityState.Deleted;
            return await this.context.SaveChangesAsync();
        }
    }
}