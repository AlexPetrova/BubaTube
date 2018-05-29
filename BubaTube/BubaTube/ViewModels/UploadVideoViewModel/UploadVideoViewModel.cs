using BubaTube.Data.DTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BubaTube.ViewModels.UploadVideoViewModel
{
    public class UploadVideoViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IFormFile Video { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; }
    }
}
