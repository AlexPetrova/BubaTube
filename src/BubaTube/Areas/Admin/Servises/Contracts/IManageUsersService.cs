using System.Threading.Tasks;

namespace BubaTube.Areas.Admin.Servises.Contracts
{
    public interface IManageUsersService
    {
        Task<int> CloseAccount(string userEmail);


    }
}
