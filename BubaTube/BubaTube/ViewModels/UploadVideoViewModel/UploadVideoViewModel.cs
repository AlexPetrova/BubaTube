using Microsoft.AspNetCore.Http;

namespace BubaTube.ViewModels.UploadVideoViewModel
{
    public class UploadVideoViewModel
    {
        public string Title { get; set; }

        public IFormFile Video { get; set; }
        
        //category
    }
}
