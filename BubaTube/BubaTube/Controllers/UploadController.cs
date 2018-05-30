using BubaTube.Services.Contracts;
using BubaTube.ViewModels.UploadVideoViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BubaTube.Controllers
{
    public class UploadController : Controller
    {
        //service to process the path and map the dto to the db model
        private IUploadVideoService uploadVideoService;

        public UploadController(IUploadVideoService uploadVideoService)
        {
            this.uploadVideoService = uploadVideoService;
        }

        public IActionResult UploadVideo()
        {
            return this.PartialView("_UploadVideo");
        }

        [HttpPost("UploadFiles")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(UploadVideoViewModel model, ICollection<string> categories)
        {
            var path = @"\wwwroot\video";
            
            if (ModelState.IsValid)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    path = Path.GetRandomFileName();
                    await model.Video.CopyToAsync(stream);
                }
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Test(UploadVideoViewModel model, ICollection<string> categories)
        {
            return Ok();
        }
    }
}
