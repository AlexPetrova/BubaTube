using System.ComponentModel.DataAnnotations;

namespace BubaTube.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
