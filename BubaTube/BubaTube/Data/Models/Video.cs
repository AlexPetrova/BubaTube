using BubaTube.Data.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Data.Models
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; } //or path?

        public Category Category { get; set; }

        public double Likes { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<UserVideo> UserVideo { get; set; }
    }
}
