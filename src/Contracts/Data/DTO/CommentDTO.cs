using System.ComponentModel.DataAnnotations;

namespace Contracts.Data.DTO
{
    public class CommentDTO
    {
        [Required(ErrorMessage = "Comment content is required.")]
        [StringLength(1000, MinimumLength = 4)]
        public string Content { get; set; }

        public double Likes { get; set; }

        [Required(ErrorMessage = "Cannot save comment without user id.")]
        public int UserId { get; set; }
    }
}
