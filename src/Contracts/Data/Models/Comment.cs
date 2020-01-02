using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Comment content is required.")]
        [StringLength(1000, MinimumLength = 4)]
        public string Content { get; set; }

        public double Likes { get; set; }

        public ICollection<Comment> Comments { get; set; }

        [Required(ErrorMessage = "Cannot save comment without user id.")]
        public int UserId { get; set; }

        public User User { get; set; }

        // fk to video
    }
}
