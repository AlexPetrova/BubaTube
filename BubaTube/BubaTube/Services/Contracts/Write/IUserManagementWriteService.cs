using System.Threading.Tasks;

namespace BubaTube.Services.Contracts.Write
{
    public interface IUserManagementWriteService
    {
        Task<int> SaveLoginDate(string userID);
    }
}
