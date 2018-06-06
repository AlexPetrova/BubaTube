using System.ComponentModel.DataAnnotations;

namespace BubaTube.Data.DTO
{
    public class UserDTO
    {
        [Required]
        [StringLength(15, ErrorMessage = "First name cannot be more than 15 symbols."),
               MinLength(2, ErrorMessage = "First name cannot be less than 2 symbols")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Last name cannot be more than 15 symbols."),
            MinLength(2, ErrorMessage = "Last name cannot be less than 2 symbols")]
        public string LastName { get; set; }

        public string Email { get; set; }

        public byte[] AvatarImage { get; set; }
    }
}
