using BubaTube.Data.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Data.Models
{
    public class Video
    {
        public string Name { get; set; } //or path?

        public Category Category { get; set; }

        public double Likes { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
