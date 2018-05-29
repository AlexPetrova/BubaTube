using BubaTube.Data.DTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BubaTube.ViewModels.UploadVideoViewModel
{
    public class UploadVideoViewModel
    {
        [Required]
        [StringLength(200, ErrorMessage ="The title of the video cannot be more than 200 and less than 5 symbols.", MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile Video { get; set; }

        //separeta parameter
        public ICollection<string> Categories { get; set; }
    }
}
