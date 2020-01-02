using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Data.DTO
{
    public class VideoDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "", MinimumLength = 5)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Path { get; set; }

        public double Likes { get; set; }

        [Required]
        public string AuthorUserName { get; set; }

        [Required]
        public IEnumerable<string> Categories { get; set; }
    }
}
