using System.ComponentModel.DataAnnotations;

namespace BubaTube.Data.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(100)]
        public string Content { get; set; }
    }
}
