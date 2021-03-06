﻿using System;

namespace Contracts.Data.Models
{
    public class BlackList : BaseModel
    {
        public int Id { get; set; }

        public string IP { get; set; }

        public string Email { get; set; }

        public DateTime AddedOn { get; set; }

        public string Reason { get; set; }

        public User AddedBy { get; set; }
    }
}
