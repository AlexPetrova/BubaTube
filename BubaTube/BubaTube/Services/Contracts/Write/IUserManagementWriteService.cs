using System.Threading.Tasks;

namespace BubaTube.Services.Contracts.Write
{
    public interface IUserManagementWriteService
    {
        Task<bool> SaveLoginDate(string userID);
    }
}
