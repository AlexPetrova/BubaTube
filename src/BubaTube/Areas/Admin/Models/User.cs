using System;

namespace BubaTube.Areas.Admin.Models
{
    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public byte[] AvatarImage { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime LastLogin { get; set; }
    }
}
