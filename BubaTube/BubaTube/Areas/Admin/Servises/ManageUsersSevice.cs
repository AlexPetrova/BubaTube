using BubaTube.Areas.Admin.Servises.Contracts;
using BubaTube.Data;
using BubaTube.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BubaTube.Areas.Admin.Servises
{
    public class ManageUsersSevice : IManageUsersService
    {
        private BubaTubeDbContext cotext;
        private UserManager<User> userManager;

        public ManageUsersSevice(
            BubaTubeDbContext context,
            UserManager<User> userManager)
        {
            this.cotext = context;
            this.userManager = userManager;
        }

        public async Task<int> CloseAccount(string userEmail)
        {
            var user =await this.userManager.FindByEmailAsync(userEmail);
            this.cotext.Entry(user).State = EntityState.Deleted;
            return await this.cotext.SaveChangesAsync();
        }
    }
}