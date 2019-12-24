using DataAccess;
using Services.Contracts.Write;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Write
{
    public class UserCommands : IUserCommands
    {
        private BubaTubeDbContext context;

        public UserCommands(BubaTubeDbContext context)
        {
            this.context = context;
        }

        public async Task<int> SaveLoginDate(string userID)
        {
            this.context.Users
                .FirstOrDefault(x => x.Id == userID)
                .LastLogin = DateTime.Now;
            var result = await context.SaveChangesAsync();

            return result;
        }
    }
}
