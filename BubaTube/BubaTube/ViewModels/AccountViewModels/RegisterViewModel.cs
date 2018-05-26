using System.ComponentModel.DataAnnotations;

namespace BubaTube.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name="Username")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
        public string Username { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Your first name cannot be more than 15 symbols."),
            MinLength(2, ErrorMessage = "First name cannot be less than 2 symbols")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Your last name cannot be more than 15 symbols."),
            MinLength(2, ErrorMessage = "Your last name cannot be less than 2 symbols")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
