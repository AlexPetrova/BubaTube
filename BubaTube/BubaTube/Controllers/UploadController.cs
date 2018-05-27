using BubaTube.ViewModels.UploadVideoViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Controllers
{
    public class UploadController : Controller
    {
        //service to process the path and map the dto to the db model
        public UploadController()
        {

        }

        public IActionResult UploadVideo()
        {
            return this.PartialView();
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(UploadVideoViewModel model)
        {
            var path = Path.GetRandomFileName();

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.Video.CopyToAsync(stream);
            }
            return Ok();
        }
    }
}
