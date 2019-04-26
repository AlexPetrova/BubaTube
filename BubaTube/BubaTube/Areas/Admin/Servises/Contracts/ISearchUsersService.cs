using BubaTube.Data.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BubaTube.Areas.Admin.Servises.Contracts
{
    public interface ISearchUsersService
    {
        IEnumerable<UserDTO> ByLastActivity(int months);

        IEnumerable<UserDTO> RegisteredAfter(DateTime fromDate);

        IEnumerable<UserDTO> RegisterdInPeriod(DateTime fromDate, DateTime toDate);

        Task<UserDTO> ByUserId(string userId);

        Task<UserDTO> ByEmail(string email);

        IEnumerable<UserDTO> ByNames(string firstName, string lastName);
    }
}
