using System;

namespace BubaTube.Data.Models
{
    public class BlackList
    {
        public int Id { get; set; }

        public string IP { get; set; }

        public string Email { get; set; }

        public DateTime AddedOn { get; set; }

        public string Reason { get; set; }

        public User AddedBy { get; set; }
    }
}
