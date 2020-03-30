using Contracts.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Tests.MockData
{
    internal class UserMockData
    {
        internal static IReadOnlyCollection<User> GetListOfUserModels()
        {
            return new List<User>
            {
                new User
                {
                    FirstName = "First_Name_01",
                    LastName = "Last_Name_01",
                    Email = "First_Mail_01@abv.bg",
                    LastLogin = DateTime.Now.AddMonths(1 * -1).AddHours(1),
                    RegisteredOn = DateTime.Now.AddMonths(1 * -1),
                    AvatarImage = new byte[1]
                },
                new User
                {
                    FirstName = "First_Name_02",
                    LastName = "Last_Name_02",
                    Email = "First_Mail_02@abv.bg",
                    LastLogin = DateTime.Now.AddMonths(1 * -3).AddHours(1),
                    RegisteredOn = DateTime.Now.AddMonths(1 * -3),
                    AvatarImage = new byte[1]
                },
                new User
                {
                    FirstName = "First_Name_03",
                    LastName = "Last_Name_03",
                    Email = "First_Mail_03@abv.bg",
                    LastLogin = DateTime.Now.AddMonths(1 * -3).AddHours(1),
                    RegisteredOn = DateTime.Now.AddMonths(1 * -3),
                    AvatarImage = new byte[1]
                },
                new User
                {
                    FirstName = "First_Name_04",
                    LastName = "Last_Name_04",
                    Email = "First_Mail_04@abv.bg",
                    LastLogin = DateTime.Now.AddMonths(1 * -4).AddHours(1),
                    RegisteredOn = DateTime.Now.AddMonths(1 * -4),
                    AvatarImage = new byte[1]
                },
                new User
                {
                    FirstName = "First_Name_05",
                    LastName = "Last_Name_05",
                    Email = "First_Mail_05@abv.bg",
                    LastLogin = DateTime.Now.AddMonths(1 * -4).AddHours(1),
                    RegisteredOn = DateTime.Now.AddMonths(1 * -4),
                    AvatarImage = new byte[1]
                },
                new User
                {
                    FirstName = "First_Name_06",
                    LastName = "Last_Name_06",
                    Email = "First_Mail_06@abv.bg",
                    LastLogin = DateTime.Now.AddMonths(1 * -4).AddHours(1),
                    RegisteredOn = DateTime.Now.AddMonths(1 * -4),
                    AvatarImage = new byte[1]
                }
            };
        }
    }
}
