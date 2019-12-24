using System.Threading.Tasks;

namespace Services.Contracts.Write
{
    public interface IUserCommands
    {
        Task<int> SaveLoginDate(string userID);

        Task<int> CloseAccount(string userEmail);
    }
}
