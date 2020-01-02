using System.ComponentModel.DataAnnotations;

namespace Contracts.Data.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The lenght of the category name cannot be more than 100 symbols.")]
        public string CategoryName { get; set; }
    }
}
