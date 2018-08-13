using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BubaTube.Data.Models
{
    public class Video
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "", MinimumLength = 5)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public string Path { get; set; } 
        
        public double Likes { get; set; }

        [Required]
        public string AuthorId { get; set; }
        
        public User Author { get; set; }

        public bool IsАpproved { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<UserVideo> UserVideo { get; set; }

        public ICollection<VideoCategory> VideoCategory { get; set; }
    }
}
