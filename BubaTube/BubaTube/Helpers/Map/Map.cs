using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Helpers.Map
{
    public static class Map
    {
        public static Func<User, UserDTO> User()
        {
            return x => new UserDTO()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                LastLogin = x.LastLogin,
                RegisteredOn = x.RegisteredOn,
                AvatarImage = x.AvatarImage
            };
        }
    }
}
