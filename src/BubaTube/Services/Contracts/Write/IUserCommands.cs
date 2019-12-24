using System.Threading.Tasks;

namespace BubaTube.Services.Contracts.Write
{
    public interface IUserCommands
    {
        Task<int> SaveLoginDate(string userID);
    }
}
