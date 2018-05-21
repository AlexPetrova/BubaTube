using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public double Likes { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        // fk to video
    }
}
