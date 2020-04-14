using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Data.Models
{
    public class Video : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The title of video cannot be less than 5 or more tha 200 symbols.", MinimumLength = 5)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public string FileName { get; set; }

        public double Likes { get; set; }

        //TODO add views property and add migration
        //public double Views { get; set; }

        [Required]
        public string AuthorId { get; set; }
        
        public User Author { get; set; }

        public bool IsАpproved { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<UserVideo> UserVideo { get; set; }

        public ICollection<VideoCategory> VideoCategory { get; set; }   
    }
}
