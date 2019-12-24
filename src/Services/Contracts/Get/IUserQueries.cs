using Contracts.Data.DTO;
using System;
using System.Collections.Generic;

namespace Services.Contracts.Get
{
    public interface IUserQueries
    {
        IReadOnlyCollection<UserDTO> RegisterdInPeriod(DateTime fromDate, DateTime toDate);

        IReadOnlyCollection<UserDTO> RegisteredAfter(DateTime date);

        IReadOnlyCollection<UserDTO> ByLastActivity(int months);
    }
}
