using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Data.DTO
{
    public class UserPreviewDTO
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] ProfilePicture { get; set; }
    }
}
