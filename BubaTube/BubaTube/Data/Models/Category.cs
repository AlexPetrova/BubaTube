using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BubaTube.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public ICollection<VideoCategory> VideoCategory { get; set; }
    }
}
