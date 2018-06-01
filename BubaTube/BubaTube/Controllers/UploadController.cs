using BubaTube.Services.Contracts;
using BubaTube.ViewModels.UploadVideoViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [Authorize]
        public IActionResult UploadVideo()
        {
            return this.PartialView("_UploadVideo");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(UploadVideoViewModel model, IFormFile video, ICollection<string> categories)
        {
            var path = @"\wwwroot\video";

            if (ModelState.IsValid)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    path = Path.GetRandomFileName();
                    //await model.Video.CopyToAsync(stream);
                }
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Test(UploadVideoViewModel model, ICollection<string> categories)
        {
            bool someCondition = true;
            if (someCondition)
            {
                return this.PartialView("_SuccessResponse");
            }
            else
            {
                return this.PartialView("_ErrorResponse");
            }
        }
    }
}
