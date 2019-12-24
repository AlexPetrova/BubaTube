using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The lenght of the category name cannot be more than 100 symbols.")]
        public string CategoryName { get; set; }

        public bool IsАpproved { get; set; }

        public ICollection<VideoCategory> VideoCategory { get; set; }
    }
}
